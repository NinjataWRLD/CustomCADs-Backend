using CustomCADs.Gallery.Domain.Carts.Entities;

namespace CustomCADs.Gallery.Domain.Carts.Reads;

public record CartResult(int Count, ICollection<Cart> Carts);