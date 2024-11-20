using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Gallery.Persistence;

public class GalleryContext(DbContextOptions<GalleryContext> opts) : DbContext(opts)
{
    public required DbSet<Cart> Carts { get; set; }
    public required DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Gallery");
        builder.ApplyConfigurationsFromAssembly(GalleryPersistenceReference.Assembly);
    }
}
