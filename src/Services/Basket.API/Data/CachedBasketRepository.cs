namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, HybridCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var cachedBasket = await cache.GetOrCreateAsync(
            userName,
            async cancellationToken =>
            {
                return await repository.GetBasketAsync(userName, cancellationToken);
            }
        );
        return cachedBasket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(
        ShoppingCart basket,
        CancellationToken cancellationToken = default
    )
    {
        var storedBasket = await repository.StoreBasketAsync(basket, cancellationToken);
        await cache.SetAsync(basket.UserName, storedBasket);
        return storedBasket;
    }

    public async Task<bool> DeleteBasketAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        await repository.DeleteBasketAsync(userName, cancellationToken);
        await cache.RemoveAsync(userName);
        return true;
    }
}
