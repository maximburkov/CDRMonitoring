namespace CDRMonitorig.Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        private PhoneNumber(string number)
        {
            Number = number;
        }

        // TODO: add validation
        public string Number { get; }

        public static PhoneNumber From(string number) => new (number);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }

        public override string ToString() => Number;
    }
}
