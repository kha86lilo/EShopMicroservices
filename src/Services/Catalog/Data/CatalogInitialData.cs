using Marten.Schema;

namespace Catalog.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store(GetPreConfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreConfiguredProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "IPhone X",
                Description =
                    "This phone is the company's biggest change to its flagship smartphone in years.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string> { "Smart Phone" },
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung 10",
                Description =
                    "This phone is Samsung's biggest change to its flagship smartphone in years.",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Category = new List<string> { "Smart Phone" },
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Huawei Plus",
                Description =
                    "This phone is Huawei's biggest change to its flagship smartphone in years.",
                ImageFile = "product-3.png",
                Price = 650.00M,
                Category = new List<string> { "Smart Phone" },
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Xiaomi Mi 11",
                Description =
                    "High performance smartphone with advanced camera features and fast charging.",
                ImageFile = "product-4.png",
                Price = 699.00M,
                Category = new List<string> { "Smart Phone" },
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "OnePlus 9 Pro",
                Description =
                    "Premium flagship phone with smooth display and excellent performance.",
                ImageFile = "product-5.png",
                Price = 799.00M,
                Category = new List<string> { "Smart Phone" },
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 6",
                Description =
                    "Google's latest smartphone with advanced AI photography capabilities.",
                ImageFile = "product-6.png",
                Price = 599.00M,
                Category = new List<string> { "Smart Phone" },
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sony Xperia 5",
                Description =
                    "Compact flagship with professional camera features and stunning display.",
                ImageFile = "product-7.png",
                Price = 899.00M,
                Category = new List<string> { "Smart Phone" },
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Nokia 8.3",
                Description =
                    "Reliable 5G smartphone with pure Android experience and long battery life.",
                ImageFile = "product-8.png",
                Price = 549.00M,
                Category = new List<string> { "Smart Phone" },
            },
        };
    }
}
