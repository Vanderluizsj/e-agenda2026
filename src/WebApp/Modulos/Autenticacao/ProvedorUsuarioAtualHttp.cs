
using System.Security.Claims;
using Dominio.Compartilhado;

namespace WebApp.Modulos.Autenticacao;

public sealed class ProvedorUsuarioAtualHttp(IHttpContextAccessor httpContextAccessor) 
: IProvedorUsuarioAtual
{
    public Guid Id
    {
        get
        {
            // No sistema de Clains, o Id do usuário logado é armazenado no ClaimTypes.NameIdentifier
            string? contextId = httpContextAccessor.HttpContext?
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(contextId, out Guid usuarioId ) ? usuarioId : Guid.Empty;
        }
    }
}
