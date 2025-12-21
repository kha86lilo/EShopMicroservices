namespace Ordering.API.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/orders",
                async ([AsParameters] PaginationRequest paginationRequest, ISender sender) =>
                {
                    var query = new GetOrdersQuery(paginationRequest);
                    var result = await sender.Send(query);

                    var response = result.Adapt<GetOrdersResponse>();
                    return Results.Ok(response);
                }
            )
            .WithName("GetOrders")
            .WithTags("Orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Gets a paginated list of orders.");
    }
}
