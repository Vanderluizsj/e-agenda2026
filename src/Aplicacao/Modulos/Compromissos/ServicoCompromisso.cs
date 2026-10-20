using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;

namespace eAgenda.Aplicacao.Modulos.Compromissos;

public sealed class ServicoCompromisso(
    IRepositorioCompromisso repositorioCompromisso,
    IRepositorioContato repositorioContato
)
{
    public Result<Guid> Cadastrar(CadastrarCompromissoDto dto)
    {
        Contato? contato = null;

        if (dto.IdContato.HasValue && dto.IdContato.Value != Guid.Empty)
            contato = repositorioContato.SelecionarPorId(dto.IdContato.Value);

        Compromisso compromisso = new(
            dto.Assunto,
            dto.DataDeOcorrencia,
            dto.HoraDeInicio,
            dto.HoraDeTermino,
            dto.Tipo,
            dto.Local,
            contato
        );

        List<ErroValidacao> erros = compromisso.Validar();

        if (repositorioCompromisso.ExisteCompromissoNaDataEHora(
            compromisso.DataDeOcorrencia,
            compromisso.HoraDeInicio,
            compromisso.HoraDeTermino))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.HoraDeInicio),
                Mensagem = "Já existe um compromisso cadastrado nesse horário."
            });
        }

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
                .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioCompromisso.Cadastrar(compromisso);

        return Result.Ok(compromisso.Id);
    }

    public Result Editar(EditarCompromissoDto dto)
    {
        Contato? contato = null;

        if (dto.IdContato.HasValue && dto.IdContato.Value != Guid.Empty)
            contato = repositorioContato.SelecionarPorId(dto.IdContato.Value);

        Compromisso compromissoAtualizado = new(
            dto.Assunto,
            dto.DataDeOcorrencia,
            dto.HoraDeInicio,
            dto.HoraDeTermino,
            dto.Tipo,
            dto.Local,
            contato
        );

        List<ErroValidacao> erros = compromissoAtualizado.Validar();

        if (repositorioCompromisso.ExisteCompromissoNaDataEHora(
            compromissoAtualizado.DataDeOcorrencia,
            compromissoAtualizado.HoraDeInicio,
            compromissoAtualizado.HoraDeTermino,
            dto.Id))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.HoraDeInicio),
                Mensagem = "Já existe um compromisso cadastrado nesse horário."
            });
        }

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
                .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        bool conseguiuEditar = repositorioCompromisso.Editar(dto.Id, compromissoAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Não foi possível editar o compromisso.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioCompromisso.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível encontrar o compromisso informado.");

        return Result.Ok();
    }

    public Result<CompromissoDto> SelecionarPorId(Guid id)
    {
        Compromisso? compromisso = repositorioCompromisso.SelecionarPorId(id);

        if (compromisso == null)
            return Result.Fail("Compromisso não encontrado.");

        return Result.Ok(new CompromissoDto(
            compromisso.Id,
            compromisso.Assunto,
            compromisso.DataDeOcorrencia,
            compromisso.HoraDeInicio,
            compromisso.HoraDeTermino,
            compromisso.Tipo,
            compromisso.Local,
            compromisso.Contato is null
                ? null
                : new ContatoDto(
                    compromisso.Contato.Id,
                    compromisso.Contato.Nome,
                    compromisso.Contato.Telefone,
                    compromisso.Contato.Email,
                    compromisso.Contato.Cargo,
                    compromisso.Contato.Empresa
                )
        ));
    }

    public List<CompromissoDto> SelecionarTodos()
    {
        List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

        return compromissos
            .Select(c => new CompromissoDto(
                c.Id,
                c.Assunto,
                c.DataDeOcorrencia,
                c.HoraDeInicio,
                c.HoraDeTermino,
                c.Tipo,
                c.Local,
                c.Contato is null
                    ? null
                    : new ContatoDto(
                        c.Contato.Id,
                        c.Contato.Nome,
                        c.Contato.Telefone,
                        c.Contato.Email,
                        c.Contato.Cargo,
                        c.Contato.Empresa
                    )
            ))
            .ToList();
    }
}

