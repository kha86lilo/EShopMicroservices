namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart?> GetBasketAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var basket = await session
            .Query<ShoppingCart>()
            .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
        return basket is null ? throw new BasketNotFoundException(userName) : basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(
        ShoppingCart basket,
        CancellationToken cancellationToken = default
    )
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task DeleteBasketAsync(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        session.DeleteWhere<ShoppingCart>(x => x.UserName == userName);
        await session.SaveChangesAsync(cancellationToken);
    }
}
