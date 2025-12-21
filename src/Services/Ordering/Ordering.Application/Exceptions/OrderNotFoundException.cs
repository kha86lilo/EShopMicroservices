using System;
using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException(OrderId orderId)
    : NotFoundException($"Order with ID '{orderId}' was not found.")
{
    public override string Message => $"Order with ID '{orderId}' was not found.";
}
