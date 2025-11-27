namespace Catalog.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    List<string> Category,
    string ImageFile
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool Success);

internal class UpdateProductCommandHandler(
    ILogger<UpdateProductCommandHandler> logger,
    IDocumentSession session
) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Updating product with ID {ProductId}", command.Id);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException($"Product with ID {command.Id} not found.");
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
