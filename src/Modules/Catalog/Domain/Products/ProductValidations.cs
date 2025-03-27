namespace CustomCADs.Catalog.Domain.Products;

using static ProductConstants;

public static class ProductValidations
{
    public static Product ValidateName(this Product product)
    {
        string property = "Name";
        string name = product.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw CustomValidationException<Product>.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw CustomValidationException<Product>.Length(property, minLength, maxLength);
        }

        return product;
    }

    public static Product ValidateDescription(this Product product)
    {
        string property = "Description";
        string description = product.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw CustomValidationException<Product>.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw CustomValidationException<Product>.Length(property, minLength, maxLength);
        }

        return product;
    }

    public static Product ValidatePrice(this Product product)
    {
        string property = "Price";
        decimal amount = product.Price;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw CustomValidationException<Product>.Range(property, min, max);
        }

        return product;
    }
}
