﻿namespace CustomCADs.Shared.Application.Payment.Dtos;

public class PurchaseInfo
{
    public string Product { get; set; } = null!;
    public decimal Price { get; set; }
    public string Buyer { get; set; } = null!;
    public string Seller { get; set; } = null!;
}
