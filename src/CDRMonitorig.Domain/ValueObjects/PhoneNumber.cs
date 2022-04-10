namespace CDRMonitorig.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private PhoneNumber(string number)
        {
            Number = number;
        }

        // TODO: add validation
        public string Number { get; set; }

        public static PhoneNumber From(string number) => new (number);
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
