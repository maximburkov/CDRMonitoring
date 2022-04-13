using CDRMonitorig.Console.Extensions;
using CDRMonitorig.Domain;
using CDRMonitorig.Domain.Rules.DialedSameNumber;
using CDRMonitorig.Domain.Rules.FromSameCaller;
using CDRMonitorig.Infrastructure.Persistence;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;


var builder = CoconaApp.CreateBuilder();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File("log.txt")
    .WriteTo.Console(LogEventLevel.Warning)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging .AddSerilog();

builder.Services.AddScoped<ICallDetailsRepository, CsvCallDetailsRepository>();
builder.Services.AddSingleton<CallDetailsService>();
builder.Services.AddSingleton<FileService>();

var app = builder.Build();


app.AddCommand("info", async ([Argument]string filename, 
    CallDetailsService callDetailsService, 
    FileService fileService, ILogger<Program> logger) =>
{
    fileService.Filename = filename;

    var info = await callDetailsService.GetTotalInformation();

    Console.WriteLine("Total Information:\n");
    Console.WriteLine($"Calls: {info.Count}");
    Console.WriteLine($"Duration: {(int)info.Duration.TotalMinutes}");
    Console.WriteLine($"Cost: {Math.Round(info.Cost, 2)}");
});

app.AddSubCommand("check", x =>
{
    x.AddCommand("DialedSameNumber", async ([Argument] string filename, 
        CallDetailsService callDetailsService,
        FileService fileService) =>
    {
        fileService.Filename = filename;

        var rule = new DialedSameNumberRule();
        var report = await callDetailsService.GetReportForRule(rule);

        report.ToConsoleOutput();
    });

    x.AddCommand("FromSameNumber", async ([Argument] string filename, 
        CallDetailsService callDetailsService,
        FileService fileService) =>
    {
        fileService.Filename = filename;

        var rule = new FromSameCallerRule();
        var report = await callDetailsService.GetReportForRule(rule);

        report.ToConsoleOutput();
    });
});

app.Run();