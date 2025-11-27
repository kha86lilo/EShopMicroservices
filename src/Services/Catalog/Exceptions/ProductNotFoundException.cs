namespace Catalog.Exceptions;

[Serializable]
internal class ProductNotFoundException : Exception
{
    public ProductNotFoundException()
        : base("Product not found.") { }

    public ProductNotFoundException(string? message)
        : base(message) { }
}
