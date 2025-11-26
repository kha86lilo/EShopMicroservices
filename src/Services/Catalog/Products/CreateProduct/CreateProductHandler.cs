using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    List<string> Category
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid ProductId);

internal class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
        };

        //TODO: Save product to database

        return new CreateProductResult(product.Id); 
    }
}
