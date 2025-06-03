using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetStatus.Reported;

public class SetProductReportedStatusUnitTests : ProductsBaseUnitTests
{
	[Fact]
	public void SetReportedStatus_ShouldNotThrowException_WhenStatusIsValid()
	{
		Assert.Multiple(
			() => CreateProduct().SetReportedStatus(),
			() => CreateProduct().SetValidatedStatus().SetReportedStatus()
		);
	}

	[Fact]
	public void SetReportedStatus_ShouldThrowException_WhenStatusIsNotValid()
	{
		Assert.Multiple(
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetReportedStatus().SetReportedStatus()),
			() => Assert.Throws<CustomValidationException<Product>>(() => CreateProduct().SetReportedStatus().SetRemovedStatus().SetReportedStatus())
		);
	}
}
