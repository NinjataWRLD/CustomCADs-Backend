using CustomCADs.Customizations.Application.Customizations.Commands.Internal.Delete;
using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Customizations.Application.Customizations.Commands.Internal.Delete;

using static CustomizationsData;

public class DeleteCustomizationHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly DeleteCustomizationHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IWrites<Customization>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Customization customization = CreateCustomization();

	public DeleteCustomizationHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(customization);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteCustomizationCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteCustomizationCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(customization), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Customization);
		DeleteCustomizationCommand command = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Customization>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
