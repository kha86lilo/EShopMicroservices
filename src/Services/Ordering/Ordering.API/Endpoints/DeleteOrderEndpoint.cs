namespace Ordering.API.Endpoints;W

public record DeleteOrderResponse(bool Success);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/orders/{id}",
                async (Guid id, ISender sender) =>
                {
                    // Map DeleteOrderRequest to DeleteOrderCommand
                    var command = new DeleteOrderCommand(id);
                    // Call the application service to Delete the order
                    var result = await sender.Send(command);

                    var response = result.Adapt<DeleteOrderResponse>();
                    // Return 201 Deleted with the new order ID
                    return Results.Ok(response);
                }
            )
            .WithName("DeleteOrder")
            .WithTags("Orders")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Deletes a new order.");
    }
}
