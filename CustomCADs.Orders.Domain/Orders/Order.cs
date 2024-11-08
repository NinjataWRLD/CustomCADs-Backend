﻿using CustomCADs.Orders.Domain.Orders.Enum;

namespace CustomCADs.Orders.Domain.Orders;

public class Order
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public string? DesignerId { get; set; }
}
