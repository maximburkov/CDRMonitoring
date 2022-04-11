using CDRMonitorig.Console.Extensions;
using CDRMonitorig.Domain;
using CDRMonitorig.Domain.Rules.DialedSameNumber;
using CDRMonitorig.Domain.Rules.FromSameCaller;
using CDRMonitorig.Infrastructure.Persistence;
using Cocona;
using Microsoft.Extensions.DependencyInjection;


var builder = CoconaApp.CreateBuilder();


builder.Services.AddScoped<ICallDetailsRepository, CsvCallDetailsRepository>();
builder.Services.AddSingleton<CallDetailsService>();
builder.Services.AddSingleton<FileService>();

var app = builder.Build();


app.AddCommand("info", async ([Argument]string filename, 
    CallDetailsService callDetailsService, 
    FileService fileService) =>
{
    fileService.Filename = filename;

    var info = await callDetailsService.GetTotalInformation();

    Console.WriteLine($"Calls: {info.Count}");
    Console.WriteLine($"Duration: {info.Duration.TotalMinutes}");
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