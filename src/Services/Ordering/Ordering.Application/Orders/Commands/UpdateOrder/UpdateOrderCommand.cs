namespace Ordering.Application.Orders.Commands;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool isSuccess);

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Order ID is required.");
        RuleFor(x => x.Order.OrderName).NotNull().WithMessage("Order name is required.");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order items are required.");
    }
}
