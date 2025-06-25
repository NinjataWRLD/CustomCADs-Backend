using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Report;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.Report;

using static CustomsData;

public class ReportCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly ReportCustomHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly AccountId designerId = AccountId.New();
	private readonly Custom custom = CreateCustom();

	public ReportCustomHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		custom.Accept(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ReportCustomCommand command = new(
			Id: ValidId,
			DesignerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ReportCustomCommand command = new(
			Id: ValidId,
			DesignerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		ReportCustomCommand command = new(
			Id: ValidId,
			DesignerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(ValidDesignerId, custom.AcceptedCustom?.DesignerId),
			() => Assert.Equal(CustomStatus.Reported, custom.CustomStatus)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		ReportCustomCommand command = new(
			Id: ValidId,
			DesignerId: designerId
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

		ReportCustomCommand command = new(
			Id: ValidId,
			DesignerId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
