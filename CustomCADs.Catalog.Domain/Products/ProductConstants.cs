namespace CustomCADs.Catalog.Domain.Products;

public static class ProductConstants
{
    public const int NameMaxLength = 18;
    public const int NameMinLength = 2;

    public const int DescriptionMaxLength = 750;
    public const int DescriptionMinLength = 10;

    public const decimal CostMin = 0.01m;
    public const decimal CostMax = 1000m;

    public const string CostMinString = "0.01";
    public const string CostMaxString = "1000";

    public const int CoordMin = -1000;
    public const int CoordMax = 1000;
}
