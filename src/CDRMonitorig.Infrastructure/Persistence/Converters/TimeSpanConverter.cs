using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CDRMonitorig.Infrastructure.Persistence.Converters
{
    public class TimeSpanConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // TODO: check if we need FromMinutes.
            return TimeSpan.FromSeconds(double.Parse(text));
        }
    }
}
