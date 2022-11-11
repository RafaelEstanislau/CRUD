using Dominio.RegraDeNegocio;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Dominio.Constantes;

namespace LivrosWEBAPI.Extensoes
{
    public static class ExtensaoProblemDetails
    {
        public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;

                        var problemDetails = new ProblemDetails
                        {
                            Instance = context.Request.HttpContext.Request.Path
                        };

                        if (exception is BadHttpRequestException badHttpRequestException)
                        {
                            problemDetails.Title = MensagensDeTela.TITULO_PROBLEM_DETAILS;
                            problemDetails.Status = StatusCodes.Status400BadRequest;
                            problemDetails.Detail = badHttpRequestException.Message;
                        }

                        else if (exception is FluentValidation.ValidationException validationException)
                        {
                            problemDetails.Title = MensagensDeTela.TITULO_PROBLEM_DETAILS;
                            problemDetails.Status = StatusCodes.Status409Conflict;
                            problemDetails.Detail = MensagensDeTela.DETALHE_PROBLEM_DETAILS;
                            var erroRetornado = validationException.Errors.Select(v => v.ErrorMessage);
                            problemDetails.Extensions.Add("erros", erroRetornado);

                        }

                        else if (exception is NotFoundException notFoundException)
                        {
                            problemDetails.Title = MensagensDeTela.TITULO_PROBLEM_DETAILS;
                            problemDetails.Status = StatusCodes.Status404NotFound;
                            problemDetails.Detail = notFoundException.Message;
                        }

                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";

                        var json = JsonSerializer.Serialize(problemDetails);
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
        public static IServiceCollection ConfigureProblemDetailsModelState(this IServiceCollection services)
        {

            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ProblemDetails()
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = MensagensDeTela.TITULO_PROBLEM_DETAILS,
                        Title = MensagensDeTela.TITULO_PROBLEM_DETAILS
                    };
                    var validationErrors = context.ModelState.Values.Where(E => E.Errors.Count > 0)
                                           .SelectMany(v => v.Errors).Select(v => v.ErrorMessage);

                    problemDetails.Extensions.Add("erros", validationErrors);

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });
        }
    }
}
