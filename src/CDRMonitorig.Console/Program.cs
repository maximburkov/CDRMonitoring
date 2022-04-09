using CDRMonitorig.Domain;
using CDRMonitorig.Infrastructure;

ICallDetailsRepository repository = new SampleRepository();
InformationService service = new InformationService(repository);

var result = await service.GetTotalInfo();

Console.WriteLine(result.ToString());