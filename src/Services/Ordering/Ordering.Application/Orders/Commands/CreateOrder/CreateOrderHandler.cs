namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(
            CreateOrderCommand command,
            CancellationToken cancellationToken
        )
        {
            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = Address.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode
            );
            var billingAddress = Address.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode
            );
            var newOrder = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                orderName: OrderName.Of(orderDto.OrderName),
                customerId: CustomerId.Of(orderDto.CustomerId),
                shippingAddress,
                billingAddress,
                payment: Payment.Of(
                    orderDto.Payment.CardHolderName,
                    orderDto.Payment.CardNumber,
                    orderDto.Payment.Expiration,
                    orderDto.Payment.Cvv,
                    orderDto.Payment.PaymentMethod.ToString()
                )
            );
            foreach (var item in orderDto.OrderItems)
            {
                newOrder.AddOrderItem(ProductId.Of(item.ProductId), item.Price, item.Quantity);
            }
            return newOrder;
        }
    }
}
