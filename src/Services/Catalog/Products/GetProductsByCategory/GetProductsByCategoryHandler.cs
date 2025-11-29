namespace Catalog.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string CategoryName)
    : IRequest<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryHandler(IDocumentSession session)
    : IRequestHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(
        GetProductsByCategoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var products = await session
            .Query<Product>()
            .Where(p => p.Category.Contains(request.CategoryName))
            .ToListAsync(cancellationToken);
        return new GetProductsByCategoryResult(products);
    }
}
