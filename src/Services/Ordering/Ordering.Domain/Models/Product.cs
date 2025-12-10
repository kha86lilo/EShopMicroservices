namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }

    public static Product Create(
        ProductId productId,
        string name,
        string description,
        decimal price
    )
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(description);
        ArgumentOutOfRangeException.ThrowIfLessThan(price, 0);

        return new Product
        {
            Id = productId,
            Name = name,
            Description = description,
            Price = price,
        };
    }
}
