using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomCancelUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, CustomValidationException<Custom>> expectValidationException
		= Assert.Throws<CustomValidationException<Custom>>;

	[Fact]
	public void Cancel_ShouldSucceed_WhenAccepted()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);

		custom.Cancel();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Cancel_ShouldSucceed_WhenBegun()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Begin();

		custom.Cancel();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Cancel_ShouldSucceed_WhenReported()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Report();

		custom.Cancel();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Cancel_ShouldFail_WhenPending()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Cancel();
		});
	}

	[Fact]
	public void Cancel_ShouldFail_WhenFinished()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Cancel();
		});
	}

	[Fact]
	public void Cancel_ShouldFail_WhenCompleted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Cancel();
		});
	}
}
