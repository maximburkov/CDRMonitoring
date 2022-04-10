using CDRMonitorig.Domain;
using CDRMonitorig.Domain.Rules.DialedSameNumber;
using CDRMonitorig.Domain.Rules.Interfaces;
using CDRMonitorig.Infrastructure.Persistence;
using Cocona;
using Microsoft.Extensions.DependencyInjection;

//ICallDetailsRepository repository = new CsvCallDetailsRepository("C:\\Users\\Maxim\\Desktop\\Test task\\FZG_Daily_Calls_LiquidEleven_07042022_34422405_9201_SIP_V3.csv");
////InformationService service = new InformationService(repository);

//CallDetailsService callDetails = new CallDetailsService(repository);

//IRule<DialedSameNumberReport> rule = new DialedSameNumberRule();

//var result = await callDetails.GetReportForRule(rule);

//var info = await callDetails.GetTotalInformation();

//Console.WriteLine(result.ToString());

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("test", () =>
{
    Console.WriteLine("test Command");
});

app.AddCommand("run", () =>
{
    Console.WriteLine("Run command");
});

app.Run();