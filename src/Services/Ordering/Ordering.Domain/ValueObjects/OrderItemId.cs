namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("OrderItemId must not be empty.", nameof(value));

        return new OrderItemId(value);
    }
}
