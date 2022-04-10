using CDRMonitorig.Domain;
using CsvHelper.Configuration;

namespace CDRMonitorig.Infrastructure.Persistence.Maps
{
    internal class CallByNameMap : ClassMap<Call>
    {
        public CallByNameMap()
        {
            Map(c => c.Caller).Name("Caller Line Identity").TypeConverter<PhoneNumberConverter>();
            Map(c => c.Dialed).Name("Non-Charged Party").TypeConverter<PhoneNumberConverter>();
            Map(c => c.SalesPrice).Name("Salesprice");
        }
    }
}
