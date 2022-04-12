using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CDRMonitorig.Domain.ValueObjects
{
    public enum Currency
    {
        Usd,
        Gbp,
        Eur
    }

    public sealed class Money : ValueObject
    {
        private Money(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public decimal Value { get; }
        public Currency Currency { get; }

        public static Money From(decimal value, Currency currency) => new (value, currency);

        private Money Add(Money other) => Currency == other.Currency
            ? new Money(Value + other.Value, Currency)
            : throw new InvalidOperationException("Different currencies couldn't be added.");
        
        private Money Subtract(Money other) => Currency == other.Currency
            ? new Money(Value - other.Value, Currency)
            : throw new InvalidOperationException("Different currencies couldn't be subtracted.");

        public static Money operator +(Money m1, Money m2) => m1.Add(m2);
        
        public static Money operator -(Money m1, Money m2) => m1.Subtract(m2);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }
    }
}
