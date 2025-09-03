using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomFinishUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, InvalidOperationException> expectValidationException
		= Assert.Throws<InvalidOperationException>;

	[Fact]
	public void Finish_ShouldSucceed_WhenBegun()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Begin();

		custom.Finish(ValidCadId, ValidPrice);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Finished, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom),
			() => Assert.NotNull(custom.FinishedCustom),
			() => Assert.Equal(ValidCadId, custom.FinishedCustom!.CadId),
			() => Assert.Equal(ValidPrice, custom.FinishedCustom!.Price)
		);
	}

	[Fact]
	public void Finish_ShouldFail_WhenInvalidPrice()
	{
		Assert.Throws<CustomValidationException<FinishedCustom>>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, InvalidPrice);
		});
	}

	[Fact]
	public void Finish_ShouldFail_WhenPending()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Fact]
	public void Finish_ShouldFail_WhenAccepted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Fact]
	public void Finish_ShouldFail_WhenReported()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Fact]
	public void Finish_ShouldFail_WhenFinished()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Fact]
	public void Finish_ShouldFail_WhenCompleted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Finish(ValidCadId, ValidPrice);
		});
	}
}
