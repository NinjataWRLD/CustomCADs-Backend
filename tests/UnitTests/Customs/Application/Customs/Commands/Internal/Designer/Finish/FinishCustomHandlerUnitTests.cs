using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.Finish;

using static CustomsData;

public class FinishCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly FinishCustomHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly AccountId designerId = AccountId.New();
	private readonly (string Key, string ContentType, decimal Volume) cad = ("generated-key", "model/gltf-binary", 15);
	private readonly Custom custom = CreateCustom();

	public FinishCustomHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateCadCommand>(x =>
				x.ContentType == cad.ContentType
				&& x.Key == x.Key
				&& x.Volume == cad.Volume
			),
			ct
		)).ReturnsAsync(ValidCadId);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		FinishCustomCommand command = new(
			Id: ValidId,
			Cad: cad,
			Price: ValidPrice,
			DesignerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		FinishCustomCommand command = new(
			Id: ValidId,
			Cad: cad,
			Price: ValidPrice,
			DesignerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		FinishCustomCommand command = new(
			Id: ValidId,
			Cad: cad,
			Price: ValidPrice,
			DesignerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(ValidCadId, custom.FinishedCustom?.CadId),
			() => Assert.Equal(CustomStatus.Finished, custom.CustomStatus)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		var custom = CreateCustom();
		custom.Accept(designerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);

		FinishCustomCommand command = new(
			Id: ValidId,
			Cad: cad,
			Price: ValidPrice,
			DesignerId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		FinishCustomCommand command = new(
			Id: ValidId,
			Cad: cad,
			Price: ValidPrice,
			DesignerId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
