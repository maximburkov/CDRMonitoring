using CDRMonitorig.Domain.ValueObjects;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CDRMonitorig.Infrastructure.Persistence.Converters
{
    public class PhoneNumberConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return PhoneNumber.From(text);
        }
    }
}
