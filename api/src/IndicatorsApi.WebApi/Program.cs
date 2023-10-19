using Carter;
using IndicatorsApi.WebApi.Configurations;
using IndicatorsApi.WebApi.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#pragma warning disable SA1312 // Variable names should begin with lower-case letter
string Cors = "Cors";
#pragma warning restore SA1312 // Variable names should begin with lower-case letter

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .InstallServices(
        builder.Configuration,
        typeof(IServiceInstaller).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors, builder =>
    {
        builder
            .WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

WebApplication app = builder.Build();

app.UseCors(Cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("v1/api/ping", () =>
{
    return Results.Ok("pong");
})
.RequireAuthorization()
.WithTags("API")
.WithName("Ping Pong");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapCarter();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.Run();
