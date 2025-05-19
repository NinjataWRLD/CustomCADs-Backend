using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.Finish;

using static CustomsData;

public class FinishCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Key = "generated-key";
	private const string ContentType = "model/gltf-binary";
	private const decimal Volume = 15;
	private const decimal Price = ValidPrice1;
	private static readonly CustomId id = ValidId1;
	private static readonly CadId cadId = ValidCadId1;
	private static readonly AccountId designerId = ValidDesignerId1;
	private static readonly AccountId wrongDesignerId = ValidDesignerId2;
	private readonly (string Key, string ContentType, decimal Volume) cad = (Key, ContentType, Volume);
	private readonly Custom custom = CreateCustom();

	public FinishCustomHandlerUnitTests()
	{
		custom.Accept(designerId);
		custom.Begin();
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendCommandAsync(
			It.IsAny<CreateCadCommand>()
		, ct)).ReturnsAsync(cadId);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		FinishCustomCommand command = new(
			Id: id,
			Cad: cad,
			Price: Price,
			DesignerId: designerId
		);
		FinishCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		FinishCustomCommand command = new(
			Id: id,
			Cad: cad,
			Price: Price,
			DesignerId: designerId
		);
		FinishCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperly()
	{
		// Arrange
		FinishCustomCommand command = new(
			Id: id,
			Cad: cad,
			Price: Price,
			DesignerId: designerId
		);
		FinishCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(cadId, custom.FinishedCustom?.CadId),
			() => Assert.Equal(CustomStatus.Finished, custom.CustomStatus)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		var custom = CreateCustom();
		custom.Accept(wrongDesignerId);
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(custom);

		FinishCustomCommand command = new(
			Id: id,
			Cad: cad,
			Price: Price,
			DesignerId: designerId
		);
		FinishCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Custom);

		FinishCustomCommand command = new(
			Id: id,
			Cad: cad,
			Price: Price,
			DesignerId: designerId
		);
		FinishCustomHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
