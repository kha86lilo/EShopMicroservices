namespace Catalog.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string CategoryName)
    : IRequest<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryHandler(
    IDocumentSession session,
    ILogger<GetProductsByCategoryHandler> logger
) : IRequestHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(
        GetProductsByCategoryQuery request,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "Handling GetProductsByCategory for CategoryName: {CategoryName}",
            request.CategoryName
        );
        var products = await session
            .Query<Product>()
            .Where(p => p.Category.Contains(request.CategoryName))
            .ToListAsync(cancellationToken);
        return new GetProductsByCategoryResult(products);
    }
}
