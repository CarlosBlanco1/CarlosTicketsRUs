using BlazorTickets.Components;
using BlazorTickets.Data;
using BlazorTickets.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SixLabors.ImageSharp;
using TicketLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped<ITicketService, WebTicketService>();
builder.Services.AddScoped<IEventService, WebEventService>();
builder.Services.AddControllers();
builder.Services.AddScoped<MailMailMail>();
builder.Services.AddSingleton<CarlosHandler>();
builder.Services.AddSingleton<CarlosMetric>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<PostgresContext>(options => options.UseNpgsql("Name=Postgres"));

builder.Services.AddHealthChecks();

const string serviceName = "carlos service";

Console.WriteLine("got here! 1");
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(b =>
    {
        b
        .AddAspNetCoreInstrumentation()
        .AddSource(CarlosTracing.traceName)
        .AddSource(CarlosTracing.traceName2)
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:4317/");
        });
    })
    .WithMetrics(b =>
    {
        b
        .AddAspNetCoreInstrumentation()
        .AddMeter(CarlosMetric.MetricName)
        .AddPrometheusExporter()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:4317/");
        });
    });

builder.Logging.AddOpenTelemetry(options =>
{
    options
    .SetResourceBuilder(
        ResourceBuilder
        .CreateDefault()
        .AddService(serviceName)
    )
    .AddOtlpExporter(o =>
    {
        o.Endpoint = new Uri("http://otel-collector:4317/");
    });
});

Console.WriteLine("got here! 2");




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//Tracing Activity endpoint

app.MapGet("/carlosCustomTrace1", () => CarlosTracing.MyActivitySource.StartActivity("CarlosActivty1"));
app.MapGet("/carlosCustomTrace2", () => CarlosTracing.MyActivitySource2.StartActivity("CarlosActivty2"));

var handler = app.Services.GetRequiredService<CarlosHandler>();

app.MapGet("/request1", () => handler.HandleRequest1());
app.MapGet("/request2", () => handler.HandleRequest2());
app.MapGet("/request3", () => handler.HandleRequest3());
app.MapGet("/request4", () => handler.HandleRequest4());
app.MapGet("/request5", () => handler.HandleRequest5());

//Metrics
var metric = app.Services.GetRequiredService<CarlosMetric>();
app.MapGet("/counterMetric", () => metric.triggerMetrics());


app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
});


app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();

public partial class Program { };
