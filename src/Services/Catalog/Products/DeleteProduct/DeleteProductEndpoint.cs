namespace Catalog.Products.DeleteProduct;

public class DeleteProductEndpoint : ICarterModule
{
    public record DeleteProductResponse(bool Success);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/products/{id:guid}",
                async (Guid id, ISender sender) =>
                {
                    var command = new DeleteProductCommand(id);

                    var result = await sender.Send(command);

                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete a product by ID");
    }
}
