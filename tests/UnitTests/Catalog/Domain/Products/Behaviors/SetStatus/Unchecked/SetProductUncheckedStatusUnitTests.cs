using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Unchecked;

public class SetProductUncheckedStatusUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void SetUncheckedStatus_ShouldNotThrowException_WhenStatusIsValid()
	{
		Assert.Multiple(
			() => CreateProduct().SetValidatedStatus().SetUncheckedStatus(),
			() => CreateProduct().SetReportedStatus().SetUncheckedStatus()
		);
	}

	[Fact]
	public void SetUncheckedStatus_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetUncheckedStatus()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetUncheckedStatus())
		);
	}
}
