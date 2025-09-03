namespace CustomCADs.Customs.Domain.Customs.States.Entities;

using static CustomConstants;

public static class Validations
{
	public static FinishedCustom ValidatePrice(this FinishedCustom custom)
	{
		custom.ThrowIfInvalidRange(
			(x) => x.Price,
			(PriceMin, PriceMax)
		);
		return custom;
	}
}
