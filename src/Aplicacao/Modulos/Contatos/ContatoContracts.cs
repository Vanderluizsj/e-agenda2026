namespace eAgenda.Aplicacao.Modulos.Contatos;

// DTO = Data Transfer Object

public record ContatoDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string Cargo,
    string Empresa
);

public record CadastrarContatoDto(
    string Nome,
    string Telefone,
    string Email,
    string Cargo,
    string Empresa
);
public record EditarContatoDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string Cargo,
    string Empresa
);