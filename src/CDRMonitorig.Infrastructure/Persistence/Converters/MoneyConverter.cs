using CDRMonitorig.Domain.ValueObjects;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CDRMonitorig.Infrastructure.Persistence.Converters
{
    internal class MoneyConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            string currencyString = row["Currency"];

            Currency currency = currencyString.ToUpperInvariant() switch
            {
                "GRB" => Currency.Gbp,
                "USD" => Currency.Usd,
                "EUR" => Currency.Eur,
                _ => throw new InvalidOperationException($"Incorrect currency: {currencyString}")
            };

            return Money.From(decimal.Parse(row["Salesprice"]), currency);
        }
    }
}
