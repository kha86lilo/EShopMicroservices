namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    List<string> Category
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid ProductId);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(200)
            .WithMessage("Product name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotEmpty()
            .WithMessage("Product description is required.")
            .MaximumLength(1000)
            .WithMessage("Product description must not exceed 1000 characters.");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Category).NotEmpty().WithMessage("At least one category is required.");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
