namespace Catalog.Exceptions;

[Serializable]
internal class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException()
        : base("Product not found.") { }

    public ProductNotFoundException(Guid id)
        : base("Product", id) { }
}
