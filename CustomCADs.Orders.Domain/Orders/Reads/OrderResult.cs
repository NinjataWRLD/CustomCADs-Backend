using CustomCADs.Orders.Domain.Orders.Entities;

namespace CustomCADs.Orders.Domain.Orders.Reads;

public record OrderResult(int Count, ICollection<Order> Orders);
