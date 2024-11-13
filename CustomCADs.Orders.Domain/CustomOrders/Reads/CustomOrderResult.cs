using CustomCADs.Orders.Domain.CustomOrders.Entities;

namespace CustomCADs.Orders.Domain.CustomOrders.Reads;

public record CustomOrderResult(int Count, ICollection<CustomOrder> Orders);
