using ChallengeBG.Application.Exceptions;

namespace ChallengeBG.Api.Middleware
{
    public class ErrorHandlerMiddleware(RequestDelegate _next, ILogger<ErrorHandlerMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await WriteErrorAsync(context, ex);
            }
        }

        private async Task WriteErrorAsync(HttpContext context, Exception ex)
        {
            var (statusCode, mensaje) = ex switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                BusinessException => (StatusCodes.Status400BadRequest, ex.Message),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado, intente nuevamente")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                _logger.LogError(ex, "Error no controlado en {Ruta}", context.Request.Path);
            else
                _logger.LogWarning("{Mensaje} en {Ruta}", ex.Message, context.Request.Path);

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { mensaje });
        }
    }
}