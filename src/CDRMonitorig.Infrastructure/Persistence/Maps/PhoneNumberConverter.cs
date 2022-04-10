using CDRMonitorig.Domain.ValueObjects;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CDRMonitorig.Infrastructure.Persistence.Maps
{
    public class PhoneNumberConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return PhoneNumber.From(text);
        }
    }
}
