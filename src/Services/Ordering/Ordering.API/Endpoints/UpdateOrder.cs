namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool Success);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
                "/orders",
                async (UpdateOrderRequest request, ISender sender) =>
                {
                    // Map UpdateOrderRequest to UpdateOrderCommand
                    var command = request.Adapt<UpdateOrderCommand>();

                    // Call the application service to Update the order
                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateOrderResponse>();
                    // Return 201 Updated with the new order ID
                    return Results.Ok(response);
                }
            )
            .WithName("UpdateOrder")
            .WithTags("Orders")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Updates a new order.");
    }
}
