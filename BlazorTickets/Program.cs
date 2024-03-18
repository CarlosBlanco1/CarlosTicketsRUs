using BlazorTickets.Components;
using BlazorTickets.Data;
using BlazorTickets.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SixLabors.ImageSharp;
using TicketLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped<ITicketService, WebTicketService>();
builder.Services.AddScoped<IEventService, WebEventService>();
builder.Services.AddControllers();
builder.Services.AddScoped<MailMailMail>();
builder.Services.AddDbContext<PostgresContext>(options => options.UseNpgsql(builder.Configuration["Postgres"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Logging.ClearProviders();

const string serviceName = "carlos service";

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
          resource.AddService(serviceName))
    .WithTracing(b =>
    {
        b
        .AddAspNetCoreInstrumentation()
        .AddSource(CarlosTracing.traceName)
        .AddSource(CarlosTracing.traceName2)
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:5317/");
        });
    })
    .WithMetrics(b =>
    {
        b
        .AddAspNetCoreInstrumentation()
        .AddMeter(CarlosMetric.MetricName)
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:5317/");
        });
    });



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

app.Run();

public partial class Program { };
