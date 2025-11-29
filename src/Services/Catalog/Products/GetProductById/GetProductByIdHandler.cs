namespace Catalog.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler(
    IDocumentSession session,
    ILogger<GetProductByIdHandler> logger
) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "Handling GetProductById for ProductId: {ProductId}",
            request.ProductId
        );
        var product = await session.LoadAsync<Product>(request.ProductId, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with ID {ProductId} not found.", request.ProductId);
            throw new ProductNotFoundException(request.ProductId);
        }
        return new GetProductByIdResult(product);
    }
}
