﻿using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.States;

using static CustomsData;

public class CustomStatesUnitTests : CustomsBaseUnitTests
{
	[Fact]
	public void Accept_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Accepted, custom.CustomStatus),
			() => Assert.Equal(ValidDesignerId, custom.AcceptedCustom?.DesignerId)
		);
	}

	[Fact]
	public void Begin_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId);
		custom.Begin();

		Assert.Equal(CustomStatus.Begun, custom.CustomStatus);
	}

	[Fact]
	public void Cancel_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId);
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

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Finished, custom.CustomStatus),
			() => Assert.Equal(ValidCadId, custom.FinishedCustom?.CadId),
			() => Assert.Equal(ValidPrice, custom.FinishedCustom?.Price)
		);
	}

	[Fact]
	public void Complete_ShouldUpdateDataProperly()
	{
		var custom = CreateCustom();

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
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
