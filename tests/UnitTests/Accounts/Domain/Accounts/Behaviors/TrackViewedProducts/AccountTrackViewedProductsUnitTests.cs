namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.TrackViewedProducts;

public class AccountTrackViewedProductsUnitTests : AccountsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(Data.AccountTrackViewedProductsValidData))]
	public void SetTrackViewedProducts_ShouldNotThrowException(bool value)
	{
		var account = CreateAccount();

		account.SetTrackViewedProducts(value);
	}

	[Theory]
	[ClassData(typeof(Data.AccountTrackViewedProductsValidData))]
	public void SetTrackViewedProducts_SetsTrackViewedProducts(bool value)
	{
		var account = CreateAccount();

		account.SetTrackViewedProducts(value);

		Assert.Equal(value, account.TrackViewedProducts);
	}
}
