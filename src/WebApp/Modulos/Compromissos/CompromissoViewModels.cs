using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Compromissos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Compromissos;

[ValidateNever]
public record CadastrarCompromissoViewModel(
    string Assunto,
    DateOnly DataDeOcorrencia,
    TimeOnly HoraDeInicio,
    TimeOnly HoraDeTermino,
    TipoCompromisso Tipo,
    string Local,
    Guid? IdContato
)
{
    public List<ContatoDto> ContatosDisponiveis { get; set; } = [];
}

[ValidateNever]
public record EditarCompromissoViewModel(
    Guid Id,
    string Assunto,
    DateOnly DataDeOcorrencia,
    TimeOnly HoraDeInicio,
    TimeOnly HoraDeTermino,
    TipoCompromisso Tipo,
    string Local,
    Guid? IdContato
)
{
    public List<ContatoDto> ContatosDisponiveis { get; set; } = [];
}

[ValidateNever]
public record ExcluirCompromissoViewModel(
    Guid Id,
    string Assunto,
    DateOnly DataDeOcorrencia,
    TimeOnly HoraDeInicio,
    TimeOnly HoraDeTermino,
    TipoCompromisso Tipo,
    string Local,
    string? NomeContato
);

