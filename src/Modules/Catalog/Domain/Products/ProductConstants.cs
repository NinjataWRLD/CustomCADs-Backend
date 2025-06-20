﻿namespace CustomCADs.Catalog.Domain.Products;

public static class ProductConstants
{
	public const int NameMaxLength = 18;
	public const int NameMinLength = 2;

	public const int DescriptionMaxLength = 750;
	public const int DescriptionMinLength = 10;

	public const decimal PriceMax = 1_000_000m;
	public const decimal PriceMin = 0.00_000_1m;
}
