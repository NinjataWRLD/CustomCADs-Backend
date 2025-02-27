using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence;

public class CartsContext(DbContextOptions<CartsContext> opts) : DbContext(opts)
{
    public required DbSet<ActiveCart> ActiveCarts { get; set; }
    public required DbSet<ActiveCartItem> ActiveCartItems { get; set; }
    public required DbSet<PurchasedCart> PurchasedCarts { get; set; }
    public required DbSet<PurchasedCartItem> PurchasedCartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Carts");
        builder.ApplyConfigurationsFromAssembly(CartsPersistenceReference.Assembly);
    }
}
