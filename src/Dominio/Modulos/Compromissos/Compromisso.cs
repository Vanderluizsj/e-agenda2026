using System.Text.RegularExpressions;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Contatos;

namespace eAgenda.Dominio.Modulos.Compromissos;

public enum TipoCompromisso
{
    Presencial,
    Online
}

public class Compromisso : EntidadeBase<Compromisso>
{
    public string Assunto { get; set; } = string.Empty;
    public DateOnly DataDeOcorrencia { get; set; } = DateOnly.MinValue;
    public TimeOnly HoraDeInicio { get; set; } = TimeOnly.MinValue;
    public TimeOnly HoraDeTermino { get; set; } = TimeOnly.MinValue;
    public TipoCompromisso Tipo { get; set; } = TipoCompromisso.Presencial;
    public string Local { get; set; } = string.Empty;
    public Contato Contato { get; set; } = new Contato();

    public Compromisso()
    {
    }

    public Compromisso(
        string assunto,
        DateOnly dataDeOcorrencia,
        TimeOnly horaDeInicio,
        TimeOnly horaDeTermino,
        TipoCompromisso tipo,
        string local,
        Contato contato
    ) : this()
    {
        Assunto = assunto;
        DataDeOcorrencia = dataDeOcorrencia;
        HoraDeInicio = horaDeInicio;
        HoraDeTermino = horaDeTermino;
        Tipo = tipo;
        Local = local;
        Contato = contato;
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Assunto) || Assunto.Length < 2 || Assunto.Length > 100)
        {
            erros.Add(
                new ErroValidacao
                {
                    Campo = nameof(Assunto),
                    Mensagem = "O campo \"Assunto\" deve conter entre 2 e 100 caracteres."
                });
        }        

        return erros;
    }

    public override void Atualizar(Compromisso entidadeAtualizada)
    {
        Assunto = entidadeAtualizada.Assunto;
        DataDeOcorrencia = entidadeAtualizada.DataDeOcorrencia;
        HoraDeInicio = entidadeAtualizada.HoraDeInicio;
        HoraDeTermino = entidadeAtualizada.HoraDeTermino;
        Tipo = entidadeAtualizada.Tipo;
        Local = entidadeAtualizada.Local;
        Contato = entidadeAtualizada.Contato;
    }
}
