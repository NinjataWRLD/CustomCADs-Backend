namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.TrackViewedProducts.Data;

public class AccountTrackViewedProductsValidData : TheoryData<bool>
{
	public AccountTrackViewedProductsValidData()
	{
		Add(true);
		Add(false);
	}
}
