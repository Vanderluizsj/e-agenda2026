using System.Text.RegularExpressions;
using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Contatos;

public class Contato : EntidadeBase<Contato>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;

    public Contato()
    {
    }

    public Contato(
        string nome,
        string telefone,
        string email,
        string cargo,
        string empresa
    ) : this()
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Cargo = cargo;
        Empresa = empresa;
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
        {
            erros.Add(
                new ErroValidacao
                {
                    Campo = nameof(Nome),
                    Mensagem = "O campo \"Nome\" deve conter entre 2 e 100 caracteres."
                });
        }

        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\)\s?\d{4,5}-\d{4}$"))
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Telefone),
                Mensagem = "O campo \"Telefone\" deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX."
            });

        if (!Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Email),
                Mensagem = "O campo \"Email\" deve estar em um formato válido."
            });

        return erros;
    }

    public override void Atualizar(Contato entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Email = entidadeAtualizada.Email;
        Cargo = entidadeAtualizada.Cargo;
        Empresa = entidadeAtualizada.Empresa;
    }
}
