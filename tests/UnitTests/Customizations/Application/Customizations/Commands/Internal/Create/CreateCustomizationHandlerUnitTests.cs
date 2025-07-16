using CustomCADs.Customizations.Application.Customizations.Commands.Internal.Create;
using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Repositories;

namespace CustomCADs.UnitTests.Customizations.Application.Customizations.Commands.Internal.Create;

using static CustomizationsData;

public class CreateCustomizationHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly CreateCustomizationHandler handler;
	private readonly Mock<IWrites<Customization>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateCustomizationHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Customization>(x =>
				x.Scale == MaxValidScale
				&& x.Infill == MaxValidInfill
				&& x.Volume == MaxValidVolume
				&& x.Color == ValidColor
				&& x.MaterialId == ValidMaterialId
			),
			ct
		)).ReturnsAsync(CreateCustomizationWithId(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateCustomizationCommand command = new(
			Scale: MaxValidScale,
			Infill: MaxValidInfill,
			Volume: MaxValidVolume,
			Color: ValidColor,
			MaterialId: ValidMaterialId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Customization>(x =>
				x.Scale == MaxValidScale
				&& x.Infill == MaxValidInfill
				&& x.Volume == MaxValidVolume
				&& x.Color == ValidColor
				&& x.MaterialId == ValidMaterialId
			),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateCustomizationCommand command = new(
			Scale: MaxValidScale,
			Infill: MaxValidInfill,
			Volume: MaxValidVolume,
			Color: ValidColor,
			MaterialId: ValidMaterialId
		);

		// Act
		CustomizationId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
