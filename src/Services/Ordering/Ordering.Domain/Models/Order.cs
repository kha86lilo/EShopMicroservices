namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public OrderName OrderName { get; private set; } = default!;
    public CustomerId CustomerId { get; private set; } = default!;

    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;

    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(oi => oi.Price * oi.Quantity);
        private set { }
    }

    public static Order Create(
        OrderId orderId,
        OrderName orderName,
        CustomerId customerId,
        Address shippingAddress,
        Address billingAddress,
        Payment payment
    )
    {
        var order = new Order
        {
            Id = orderId,
            OrderName = orderName,
            CustomerId = customerId,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
        };

        order.AddDomainEvent(new OrderCreatedDomainEvent(order));

        return order;
    }

    public void Update(
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment
    )
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;

        AddDomainEvent(new OrderUpdatedDomainEvent(this));
    }

    public void AddOrderItem(ProductId productId, decimal price, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var orderItem = new OrderItem(productId, Id, price, quantity);
        _orderItems.Add(orderItem);
        AddDomainEvent(new OrderUpdatedDomainEvent(this));
    }

    public void RemoveOrderItem(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(oi => oi.ProductId == productId);
        if (orderItem == null)
        {
            throw new InvalidOperationException("Order item not found.");
        }
        _orderItems.Remove(orderItem);
        AddDomainEvent(new OrderUpdatedDomainEvent(this));
    }
}
