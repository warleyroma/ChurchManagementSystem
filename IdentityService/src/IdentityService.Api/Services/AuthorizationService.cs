using System.Security.Claims;

namespace ControleDeAutenticaçao.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool CheckUserPermission(ClaimsPrincipal user, string requiredRole)
        {
            // Verifica se o usuário tem a permissão (função) necessária.
            return user.IsInRole(requiredRole);
        }
    }
}
