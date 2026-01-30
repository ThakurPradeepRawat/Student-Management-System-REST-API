using Common.BAL.BAL.Exceptions.Base;
using Common.DAL.Exceptions;
using Common.Model.DTO;

namespace CommonAPI.MiddleWare
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleBusinessException(context, ex);
            }
            catch (TechnicalException ex)
            {
                await HandleTechnicalException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleUnknownException(context, ex);
            }
        }

        private static async Task HandleBusinessException(HttpContext context, BusinessException ex)
        {
            context.Response.Clear();
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponseDTO
            {
                Success = false,
                ErrorCode = ex.ErrorCode,
                Message = ex.Message,
                CorrelationId = context.TraceIdentifier
            };
            await context.Response.WriteAsJsonAsync(response);

        }

        private async Task HandleTechnicalException(HttpContext context , TechnicalException ex)
        { 
            _logger.LogError(ex , "Technical Exception | ErrorCode : SYS-DB-500 | TraceID : {TraceID} ", "SYS-DB-500", context.TraceIdentifier

            );
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponseDTO
            {
                Success = false,
                ErrorCode = "SYS-DB-500",
                Message = ex.Message,
                CorrelationId = context.TraceIdentifier

            };
            await context.Response.WriteAsJsonAsync (response); 
        }


        private async Task HandleUnknownException(HttpContext context, Exception ex)
        {
            _logger.LogCritical(
                ex, "Unknown Exception | TraceID : {TraceID}", "SYS-500", context.TraceIdentifier);

            context.Response.Clear();   
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponseDTO
            {
                Success = false,
                ErrorCode = "SYS - 500",
                Message = "An un-expected error occured",
                CorrelationId = context.TraceIdentifier
            };

            await context.Response.WriteAsJsonAsync(response);
        }


    }
    
}
