using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomCancelUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, InvalidOperationException> expectValidationException
		= Assert.Throws<InvalidOperationException>;

	[Fact]
	public void Cancel_ShouldSucceed_WhenAccepted()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);

		custom.Cancel();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Pending, custom.CustomStatus),
			() => Assert.Null(custom.AcceptedCustom)
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
			() => Assert.Equal(CustomStatus.Pending, custom.CustomStatus),
			() => Assert.Null(custom.AcceptedCustom)
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
			() => Assert.Equal(CustomStatus.Pending, custom.CustomStatus),
			() => Assert.Null(custom.AcceptedCustom)
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
