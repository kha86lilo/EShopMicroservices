namespace Ordering.Domain.Models;

public class Payment
{
    public string CardNumber { get; private set; } = default!;
    public string CardHolderName { get; private set; } = default!;
    public DateTime ExpirationDate { get; private set; }
    public string CVV { get; } = default!;
    public string PaymentMethod { get; private set; } = default!;

    protected Payment() { }

    private Payment(
        string cardNumber,
        string cardHolderName,
        DateTime expirationDate,
        string cvv,
        string paymentMethod
    )
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpirationDate = expirationDate;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(
        string cardNumber,
        string cardHolderName,
        DateTime expirationDate,
        string cvv,
        string paymentMethod
    )
    {
        ArgumentException.ThrowIfNullOrEmpty(cardNumber);
        ArgumentException.ThrowIfNullOrEmpty(cardHolderName);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
        ArgumentException.ThrowIfNullOrEmpty(paymentMethod);

        return new Payment(cardNumber, cardHolderName, expirationDate, cvv, paymentMethod);
    }
}
