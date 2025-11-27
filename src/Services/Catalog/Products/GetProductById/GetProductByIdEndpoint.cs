namespace Catalog.Products.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public record GetProductByIdResponse(Product Product) : IQuery<GetProductByIdResponse>;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/{id:guid}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new GetProductByIdQuery(id);
                    var result = await sender.Send(query, cancellationToken);
                    if (result.Product == null)
                    {
                        return Results.NotFound();
                    }
                    var response = result.Adapt<GetProductByIdResponse>();
                    return Results.Ok(response);
                }
            )
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get product by ID")
            .WithDescription("Retrieves a product by its unique identifier.")
            .WithTags("Products");
    }
}
