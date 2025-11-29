namespace Catalog.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public record GetProductsRequest(int PageNumber = 1, int PageSize = 10);

    public record GetProductsResponse(IEnumerable<Product> Products) : IQuery<GetProductsResponse>;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products",
                async (
                    [AsParameters] GetProductsRequest request,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var query = request.Adapt<GetProductsQuery>();
                    var result = await sender.Send(query, cancellationToken);
                    var response = result.Adapt<GetProductsResponse>();
                    return Results.Ok(response);
                }
            )
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all products")
            .WithDescription("Retrieves a list of all products in the catalog.")
            .WithTags("Products");
    }
}
