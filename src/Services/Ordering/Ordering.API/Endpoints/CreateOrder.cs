public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/orders",
                async (CreateOrderRequest request, ISender sender) =>
                {
                    // Map CreateOrderRequest to OrderDto
                    var command = request.Adapt<CreateOrderCommand>();

                    // Call the application service to create the order
                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateOrderResponse>();
                    // Return 201 Created with the new order ID
                    return Results.Created($"/orders/{response.Id}", response);
                }
            )
            .WithName("CreateOrder")
            .WithTags("Orders")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Creates a new order.");
    }
}
