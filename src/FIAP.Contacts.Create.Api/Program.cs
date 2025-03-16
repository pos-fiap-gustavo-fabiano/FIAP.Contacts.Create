using FIAP.Contacts.Create.Infra;
using FIAP.Contacts.Create.Api;
using FIAP.Contacts.Create.Application.Shared;
using FIAP.Contacts.Create.Api.Middleware;

using FIAP.Contacts.Create.Api.Config;
using Prometheus;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddApiService();
builder.Services.AddMetricsConfig();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});
var app = builder.Build();
app.MapPrometheusScrapingEndpoint();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.UseDeveloperExceptionPage();

app.Services.CreateScope().ServiceProvider.UpdateMigrate();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.Run();

public partial class Program { }