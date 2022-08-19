using PizzaHome.Core.Helpers;
using System.Net;
using System.Text.Json;

namespace PizzaHome.API.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
            try
            { 
                await next(context);
            }
            catch (Exception ex) { 
              
               var response = context.Response;
               response.ContentType = "application/json";

                var errorResponse = new ErrorResponseHelper()
                {
                    Success = false
                };

                switch (ex) {

                    case ApplicationException e:

                        errorResponse.Message = e.Message;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case KeyNotFoundException e:

                        errorResponse.Message = e.Message;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default :

                         if (ex.Message.Contains("IDX"))
                        {
                            errorResponse.Message = "Invalid Token Provided!";
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        }

                        errorResponse.Message = ex.Message;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }

                var result = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(result);

            }
    }

      /*  private async Task HandleExceptionAsync(HttpContext context, Exception exception) {

            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse { 
                 Success = false,
            };
           

            switch (exception)
            {
                case ApplicationException e:

                    errorResponse.Message = e.Message;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case KeyNotFoundException e:

                    errorResponse.Message = e.Message;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:

                    if (exception.Message.Contains("Invalid salt version"))
                    {
                        errorResponse.Message = "Incorrect Password Provided!";
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }


                    errorResponse.Message = exception.Message;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

         
            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
     */


   }
}
