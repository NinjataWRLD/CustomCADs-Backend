using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomReportUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, CustomValidationException<Custom>> expectValidationException
		= Assert.Throws<CustomValidationException<Custom>>;

	[Fact]
	public void Report_ShouldSucceed_WhenPending()
	{
		Custom custom = CreateCustom();

		custom.Report();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Report_ShouldSucceed_WhenAccepted()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);

		custom.Report();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Report_ShouldSucceed_WhenBegun()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Begin();

		custom.Report();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Report_ShouldFail_WhenReported()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Report();
		});
	}

	[Fact]
	public void Report_ShouldFail_WhenFinished()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Report();
		});
	}

	[Fact]
	public void Report_ShouldFail_WhenCompleted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Report();
		});
	}
}
