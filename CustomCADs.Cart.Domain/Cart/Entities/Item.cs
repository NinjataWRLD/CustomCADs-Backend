﻿using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Enums;

namespace CustomCADs.Cart.Domain.Cart.Entities;

public class Item : IEntity
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Purpose Purpose { get; set; }
    public Guid ProductId { get; set; }
    public Guid CartId { get; set; }
    public required Cart Cart { get; set; }

    public decimal Cost => Price * Quantity;
}
