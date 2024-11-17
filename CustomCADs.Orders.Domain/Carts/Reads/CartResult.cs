using CustomCADs.Orders.Domain.Carts.Entities;

namespace CustomCADs.Orders.Domain.Carts.Reads;

public record CartResult(int Count, ICollection<Cart> Carts);