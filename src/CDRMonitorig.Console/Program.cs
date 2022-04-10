using CDRMonitorig.Domain;
using CDRMonitorig.Domain.Rules.DialedSameNumber;
using CDRMonitorig.Infrastructure;
using CDRMonitorig.Infrastructure.Persistence;

ICallDetailsRepository repository = new CsvCallDetailsRepository("C:\\Users\\Maxim\\Desktop\\Test task\\FZG_Daily_Calls_LiquidEleven_07042022_34422405_9201_SIP_V3.csv");
//InformationService service = new InformationService(repository);

MonitoringService monitoring = new MonitoringService(repository);

IRule<DialedSameNumberReport> rule = new DialedSameNumberRule(repository);

var result = await monitoring.ApplyRule(rule);

//var result = await service.GetTotalInfo();

Console.WriteLine(result.ToString());