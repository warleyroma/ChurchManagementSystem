using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace ControleDeAutenticaçao.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Registrar a exceção no log.
            _logger.LogError(context.Exception, "An error occurred.");

            // Definir o resultado da ação para um resultado apropriado de erro.
            context.Result = new ObjectResult("An error occurred.")
            {
                StatusCode = 500,
                DeclaredType = typeof(string)
            };
        }
    }
}
