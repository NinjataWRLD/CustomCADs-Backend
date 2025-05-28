using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.States;

using static CustomsData;

public class CustomStatesUnitTests : CustomsBaseUnitTests
{
	[Fact]
	public void Accept_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId1);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Accepted, custom.CustomStatus),
			() => Assert.Equal(ValidDesignerId1, custom.AcceptedCustom?.DesignerId)
		);
	}

	[Fact]
	public void Begin_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId1);
		custom.Begin();

		Assert.Equal(CustomStatus.Begun, custom.CustomStatus);
	}

	[Fact]
	public void Cancel_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId1);
		custom.Begin();
		custom.Cancel();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Pending, custom.CustomStatus),
			() => Assert.Null(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Finish_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId1);
		custom.Begin();
		custom.Finish(ValidCadId1, ValidPrice1);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Finished, custom.CustomStatus),
			() => Assert.Equal(ValidCadId1, custom.FinishedCustom?.CadId),
			() => Assert.Equal(ValidPrice1, custom.FinishedCustom?.Price)
		);
	}

	[Fact]
	public void Complete_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId1);
		custom.Begin();
		custom.Finish(ValidCadId1, ValidPrice1);
		custom.Complete(null);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Completed, custom.CustomStatus),
			() => Assert.NotNull(custom.CompletedCustom)
		);
	}

	[Fact]
	public void Report_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Report();

		Assert.Equal(CustomStatus.Reported, custom.CustomStatus);
	}
}
