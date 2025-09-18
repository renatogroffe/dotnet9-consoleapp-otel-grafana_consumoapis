using ConsoleAppConsumoAPIsOtel.Inputs;
using ConsoleAppConsumoAPIsOtel.Tracing;
using Grafana.OpenTelemetry;
using Microsoft.Extensions.Configuration;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
logger.Information("***** Testando o uso de OpenTelemetry com .NET e outras stacks *****");

var resourceBuilder = ResourceBuilder
    .CreateDefault()
    .AddService(OpenTelemetryExtensions.ServiceName);

var traceBuilder = Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(resourceBuilder)
    .AddSource(OpenTelemetryExtensions.ServiceName)
    .AddHttpClientInstrumentation()
    .UseGrafana();
if (Convert.ToBoolean(configuration["OtlpExporter:UseGrafana"]))
    traceBuilder.UseGrafana();
else
    traceBuilder.AddOtlpExporter(cfg =>
    {
        cfg.Endpoint = new Uri(configuration["OtlpExporter:Endpoint"]!);
    });
var traceProvider = traceBuilder.Build();

var testUrls = configuration.GetSection("TestUrls").Get<string[]>();
using var httpClient = new HttpClient();

do
{
    using var activityConsumoAPIs = OpenTelemetryExtensions.ActivitySource
        .StartActivity("ConsumoAPIs")!;
    int testNumber = 0;
    foreach (var backendUrl in testUrls!)
    {
        testNumber++;
        using var activityTestBackend = OpenTelemetryExtensions.ActivitySource
            .StartActivity($"TestBackend{testNumber}")!;
        logger.Information($"Enviando requisicao para a API: {backendUrl}");
        var response = await httpClient.GetAsync(backendUrl);
        logger.Information($"Status Code: {response.StatusCode}");
        var responseBody = await response.Content.ReadAsStringAsync();
        logger.Information($"Response Body: {responseBody}");
        logger.Information($"Consumo da API {backendUrl} concluido com sucesso!");
        activityTestBackend.SetTag("horario", $"{DateTime.UtcNow.AddHours(-3):HH:mm:ss}");
        activityTestBackend.SetTag("url", backendUrl);
        activityTestBackend.SetTag("response", responseBody);
        activityTestBackend.Stop();
    }
    activityConsumoAPIs.Stop();
} while (InputHelper.Continue());