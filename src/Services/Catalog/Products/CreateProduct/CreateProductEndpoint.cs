namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    List<string> Category
);

public record CreateProductResponse(Guid ProductId);

class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<CreateProductResponse>();
                    return Results.Created($"/products/{response.ProductId}", response);
                }
            )
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDescription("Creates a new product in the catalog.");
    }
}
