namespace CustomCADs.Customs.Domain.Customs.Entities;

using static CustomConstants;

public static class Validations
{
    public static FinishedCustom ValidatePrice(this FinishedCustom custom)
    {
        string property = "Price";
        decimal? price = custom.Price;

        decimal max = PriceMax, min = PriceMin;
        if (price > max || price < min)
        {
            throw CustomValidationException<Custom>.Range(property, max, min);
        }

        return custom;
    }
}
