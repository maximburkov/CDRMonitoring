using System.Text.RegularExpressions;

namespace CDRMonitorig.Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        private static readonly Regex PhoneNumberRegex = new ("^\\+[1-9]{1}[0-9]{3,14}$");
        private PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; }

        public static PhoneNumber From(string number) =>
            PhoneNumberRegex.IsMatch(number)
                ? new(number)
                : throw new InvalidOperationException($"Incorrect phone number {number}");
        

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }

        public override string ToString() => Number;
    }
}
