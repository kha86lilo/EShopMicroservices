namespace Ordering.API.Endpoints;

public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrderByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/orders/{name}",
                async (string name, ISender sender) =>
                {
                    var query = new GetOrdersByNameQuery(name);
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
