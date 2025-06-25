using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomBeginUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, CustomValidationException<Custom>> expectValidationException
		= Assert.Throws<CustomValidationException<Custom>>;

	[Fact]
	public void Begin_ShouldSucceed_WhenAccepted()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);

		custom.Begin();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Begin_ShouldFail_WhenPending()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenBegun()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenReported()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenFinished()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenCompleted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Begin();
		});
	}
}
