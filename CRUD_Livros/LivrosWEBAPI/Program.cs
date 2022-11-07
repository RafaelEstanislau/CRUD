
using CRUD_Livros.Infra.AcessoDeDados;
using Infra.AcessoDeDados;
using Microsoft.AspNetCore.StaticFiles;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddProblemDetails();
    builder.Services.AddControllers();
    builder.Services.AddCors();
    builder.Services.AddScoped<IRepositorio, RepositoryLINQTODB>();
}


var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
       );
    app.UseProblemDetails();
    app.UseAuthentication();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions()
    {
        ContentTypeProvider = new FileExtensionContentTypeProvider
        {
            Mappings = { [".properties"] = "application/x-msdownload" }
        }
    });
    app.MapControllers();
    app.Run();
}
