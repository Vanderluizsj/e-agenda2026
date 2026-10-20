using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Compromissos;
using FluentResults;

namespace eAgenda.Aplicacao.Modulos.Compromissos;

public sealed class ServicoCompromisso(IRepositorioCompromisso repositorioCompromisso)
{
    public Result<Guid> Cadastrar(CadastrarCompromissoDto dto)
    {
        Compromisso Compromisso = new(
            dto.Assunto,
            dto.DataDeOcorrencia,
            dto.HoraDeInicio,
            dto.HoraDeTermino,
            dto.Tipo,
            dto.Local,
            dto.IdContato
            );

        List<ErroValidacao> erros = Compromisso.Validar();

        if (repositorioCompromisso.ExisteCompromissoNaDataEHora(Compromisso.DataDeOcorrencia, Compromisso.HoraDeInicio, Compromisso.HoraDeTermino))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.DataDeOcorrencia),
                Mensagem = "Já existe um compromisso cadastrado na data e hora informadas"
            });
        }
        
        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
                .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioCompromisso.Cadastrar(Compromisso);

        return Result.Ok(Compromisso.Id);
    }

    public Result Editar(EditarCompromissoDto dto)
    {
        Compromisso compromissoAtualizado = new(dto.Assunto, dto.DataDeOcorrencia, dto.HoraDeInicio, dto.HoraDeTermino, dto.Tipo, dto.Local, dto.IdContato);

        List<ErroValidacao> erros = compromissoAtualizado.Validar();

        if (repositorioCompromisso.ExisteCompromissoNaDataEHora(compromissoAtualizado.DataDeOcorrencia, compromissoAtualizado.HoraDeInicio, compromissoAtualizado.HoraDeTermino))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.DataDeOcorrencia),
                Mensagem = "Já existe um compromisso cadastrado na data e hora informadas"
            });
        }

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
                .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioCompromisso.Editar(dto.Id, compromissoAtualizado);

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
            new ContatoDto(
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
                new ContatoDto(
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