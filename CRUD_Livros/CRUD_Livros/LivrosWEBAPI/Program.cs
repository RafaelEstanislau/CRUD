
using CRUD_Livros.Infra.AcessoDeDados;
using Infra.AcessoDeDados;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddCors();
    builder.Services.AddScoped<IRepositorio, RepositoryLINQTODB>();
}


var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
       );
    app.UseAuthentication();
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.MapControllers();
    app.Run();
}
