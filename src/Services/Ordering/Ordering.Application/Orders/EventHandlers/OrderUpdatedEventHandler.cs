using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedDomainEvent>
{
    public Task Handle(OrderUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "OrderUpdatedEventHandler handled for Order Id: {OrderId}",
            notification.order.Id
        );
        return Task.CompletedTask;
    }
}
