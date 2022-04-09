namespace CDRMonitorig.Domain.ValueObjects
{
    public class PhoneNumber
    {
        private PhoneNumber(string number)
        {
            Number = number;
        }

        // TODO: add validation
        public string Number { get; set; }

        public static PhoneNumber From(string number) => new PhoneNumber(number);
    }
}
