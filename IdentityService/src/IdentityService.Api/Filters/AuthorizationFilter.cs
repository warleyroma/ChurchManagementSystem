using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ControleDeAutenticaçao.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Implemente aqui a lógica de autorização adequada.
            // Exemplo: Verifique se o usuário tem permissão para acessar o recurso.
            
            bool userHasPermission = CheckUserPermission(context.HttpContext.User.Identity.Name);

            if (!userHasPermission)
            {
                context.Result = new ForbidResult(); // Retorna 403 Forbidden se não houver permissão.
            }
        }

        private bool CheckUserPermission(string username)
        {
            // Implemente a lógica para verificar as permissões do usuário.
            // Por exemplo, consulte um banco de dados ou algum mecanismo de autorização.

            // Exemplo simples:
            // return username == "admin";
            return false;
        }
    }
}
