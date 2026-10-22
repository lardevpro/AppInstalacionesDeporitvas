using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using AppInstalacionesDeporitvas.Data;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Gestion de Isntalaciones Deportivas",
            Version = "v1",
            Description ="API que gestiona las reservas de las diferentes instalaciones deportivas de un centro sabiendo que los datos son ficticios." 
        });
    });
    string connectionString =
    builder.Configuration.GetConnectionString("InstalacionesDeportivasMySql") ?? throw new InvalidOperationException("No se ha configurado la conexión a la base datos instalaciones-deportivas.");

    builder.Services.AddDbContext<DeporteDbContext>(options =>
    {
        options.UseMySQL(connectionString);
        options.EnableDetailedErrors();
        options.LogTo(
        Console.WriteLine,
        LogLevel.Information);
    });
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json","Instalaciones Deportivas API v1");
        });
    }
    app.UseHttpsRedirection();
    app.MapControllers();
    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DeporteDbContext>();
        await DemoDataSeeder.InicializarAsync(db);
}
app.Run();
