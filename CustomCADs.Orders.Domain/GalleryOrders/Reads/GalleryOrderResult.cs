using CustomCADs.Orders.Domain.GalleryOrders.Entities;

namespace CustomCADs.Orders.Domain.GalleryOrders.Reads;

public record GalleryOrderResult(int Count, ICollection<GalleryOrder> Orders);