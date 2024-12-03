using CustomCADs.Catalog.Domain.Common.Exceptions.Products;

namespace CustomCADs.Catalog.Domain.Products.Validation;

using static ProductConstants;

public static class ProductValidations
{
    public static Product ValidateName(this Product product)
    {
        string property = "Name";
        string name = product.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw ProductValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw ProductValidationException.Length(property, maxLength, minLength);
        }

        return product;
    }

    public static Product ValidateDescription(this Product product)
    {
        string property = "Description";
        string description = product.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw ProductValidationException.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw ProductValidationException.Length(property, maxLength, minLength);
        }

        return product;
    }

    public static Product ValidatePriceAmount(this Product product)
    {
        string property = "Price";
        decimal amount = product.Price;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw ProductValidationException.Range(property, max, min);
        }

        return product;
    }
}
