using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeAutenticaçao.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Copia o corpo da solicitação para um novo fluxo para leitura.
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;
            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            context.Request.Body = originalRequestBody;

            _logger.LogInformation($"[Request] {context.Request.Method} {context.Request.Path} - Body: {requestBodyText}");

            // Chama o próximo middleware.
            await _next(context);
        }
    }
}
