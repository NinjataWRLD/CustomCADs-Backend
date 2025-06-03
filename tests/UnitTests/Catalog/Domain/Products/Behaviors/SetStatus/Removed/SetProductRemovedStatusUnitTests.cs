using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Removed;

public class SetProductRemovedStatusUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void SetRemovedStatus_ShouldNotThrowException_WhenStatusIsValid()
	{
		CreateProduct().SetReportedStatus().SetRemovedStatus();
	}

	[Fact]
	public void SetRemovedStatus_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetRemovedStatus()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetValidatedStatus().SetRemovedStatus()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetRemovedStatus())
		);
	}
}
