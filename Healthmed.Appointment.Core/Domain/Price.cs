using System.Globalization;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.Domain
{
    public sealed record Price
    {
        public decimal Amount { get; } = 0;

        public Price(decimal amount)
        {
            InvalidPriceException.ThrowIf(amount < 0);

            Amount = amount;
        }

        public override string ToString() => Amount.ToString(CultureInfo.InvariantCulture);

        public static implicit operator decimal(Price price) => price.Amount;

        public static implicit operator Price(decimal price) => new(price);
    }
}
