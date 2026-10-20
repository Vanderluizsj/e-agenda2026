using System.ComponentModel.DataAnnotations;

namespace eAgenda.WebApp.Modulos.Contatos;

public record CadastrarContatoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\(\d{2}\)\s?\d{4,5}-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"Email\" deve ser preenchido.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O campo \"Email\" deve conter um endereço de email válido.")]
    string Email,

    string Cargo,
    string Empresa
);

public record EditarContatoViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\(\d{2}\)\s?\d{4,5}-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"Email\" deve ser preenchido.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O campo \"Email\" deve conter um endereço de email válido.")]
    string Email,
    string Cargo,
    string Empresa
);

public record ExcluirContatoViewModel(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string Cargo,
    string Empresa
);
