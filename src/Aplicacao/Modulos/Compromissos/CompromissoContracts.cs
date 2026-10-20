using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Compromissos;

namespace eAgenda.Aplicacao.Modulos.Compromissos;

// DTO = Data Transfer Object

public record CompromissoDto(
    Guid Id,
    string Assunto,
    DateOnly DataDeOcorrencia,
    TimeOnly HoraDeInicio,
    TimeOnly HoraDeTermino,
    TipoCompromisso Tipo,
    string Local,
    ContatoDto Contato
);

public record CadastrarCompromissoDto(
    string Assunto,
    DateOnly DataDeOcorrencia,
    TimeOnly HoraDeInicio,
    TimeOnly HoraDeTermino,
    TipoCompromisso Tipo,
    string Local,
    Guid IdContato
);

public record EditarCompromissoDto(
    Guid Id,
    string Assunto,
    DateOnly DataDeOcorrencia,
    TimeOnly HoraDeInicio,
    TimeOnly HoraDeTermino,
    TipoCompromisso Tipo,
    string Local,
    Guid IdContato
);

