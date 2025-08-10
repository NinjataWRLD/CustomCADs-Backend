namespace CustomCADs.Printing.Application.Customizations;

internal static class Mapper
{
	internal static CustomizationDto ToDto(this Customization customization, decimal weight, decimal cost)
		=> new(
			Id: customization.Id,
			Scale: customization.Scale,
			Infill: customization.Infill,
			Volume: customization.Volume,
			Weight: weight,
			Cost: cost,
			Color: customization.Color,
			MaterialId: customization.MaterialId
		);
}
