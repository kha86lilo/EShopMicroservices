using FluentValidation;

namespace EShopMicroservices.Services.Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Order.OrderName).NotNull().WithMessage("Order name is required.");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order items are required.");
    }
}
