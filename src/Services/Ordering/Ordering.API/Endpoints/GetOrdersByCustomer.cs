namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/orders/customer/{customerId}",
                async (Guid customerId, ISender sender) =>
                {
                    var query = new GetOrdersByCustomerQuery(customerId);
                    var result = await sender.Send(query);

                    var response = result.Adapt<IEnumerable<OrderDto>>();
                    return Results.Ok(response);
                }
            )
            .Accepts<string>("application/json")
            .WithName("GetOrderByName")
            .WithTags("Orders")
            .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Gets orders by name.");
    }
}
