using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomAcceptUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, InvalidOperationException> expectValidationException
		= Assert.Throws<InvalidOperationException>;

	[Fact]
	public void Accept_ShouldSucceed_WhenPending()
	{
		Custom custom = CreateCustom();

		custom.Accept(ValidDesignerId);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Accepted, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Accept_ShouldFail_WhenAccepted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Accept(ValidDesignerId);
		});
	}

	[Fact]
	public void Accept_ShouldFail_WhenBegun()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Accept(ValidDesignerId);
		});
	}

	[Fact]
	public void Accept_ShouldFail_WhenReported()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Accept(ValidDesignerId);
		});
	}

	[Fact]
	public void Accept_ShouldFail_WhenFinished()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Accept(ValidDesignerId);
		});
	}

	[Fact]
	public void Accept_ShouldFail_WhenCompleted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Accept(ValidDesignerId);
		});
	}
}
