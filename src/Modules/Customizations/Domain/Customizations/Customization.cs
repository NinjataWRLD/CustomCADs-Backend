using CustomCADs.Customizations.Domain.Customizations.Validation;
using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Customizations.Domain.Customizations;

public class Customization : BaseAggregateRoot
{
    private Customization() { }
    private Customization(
        decimal scale,
        decimal infill,
        decimal volume,
        string color,
        MaterialId materialId
    ) : this()
    {
        Scale = scale;
        Infill = infill;
        Volume = volume;
        Color = color;
        MaterialId = materialId;
    }

    public CustomizationId Id { get; init; }
    public decimal Scale { get; private set; }
    public decimal Infill { get; private set; }
    public decimal Volume { get; private set; }
    public string Color { get; private set; } = string.Empty;
    public MaterialId MaterialId { get; private set; }

    public static Customization Create(
        decimal scale,
        decimal infill,
        decimal volume,
        string color,
        MaterialId materialId
    ) => new Customization(
        scale: scale,
        infill: infill,
        volume: volume,
        color: color,
        materialId: materialId
    )
    .ValidateScale()
    .ValidateInfill()
    .ValidateVolume()
    .ValidateColor();

    public static Customization CreateWithId(
        CustomizationId id,
        decimal scale,
        decimal infill,
        decimal volume,
        string color,
        MaterialId materialId
    ) => new Customization(
        scale: scale,
        infill: infill,
        volume: volume,
        color: color,
        materialId: materialId
    )
    {
        Id = id
    }
    .ValidateScale()
    .ValidateInfill()
    .ValidateVolume()
    .ValidateColor();

    public Customization SetScale(decimal scale)
    {
        Scale = scale;
        this.ValidateScale();
        return this;
    }

    public Customization SetInfill(decimal infill)
    {
        Infill = infill;
        this.ValidateInfill();
        return this;
    }

    public Customization SetVolume(decimal volume)
    {
        Volume = volume;
        this.ValidateVolume();
        return this;
    }

    public Customization SetColor(string color)
    {
        Color = color;
        this.ValidateColor();
        return this;
    }

    public Customization SetMaterialId(MaterialId materialId)
    {
        MaterialId = materialId;
        return this;
    }

    /// <summary>
    ///     Calculate Customization Weight
    /// </summary>
    /// <param name="materialDensity">In g/cm³</param>
    /// <returns>Weight in grams</returns>
    public decimal CalculateWeight(decimal materialDensity)
        => (Volume / 1000) // volume in cm³
        * materialDensity
        * (CustomizationConstants.WallFactor +
            (1 - CustomizationConstants.WallFactor) * Infill
        );

    /// <summary>
    ///     Calculate Customization Cost
    /// </summary>
    /// <param name="materialDensity">In g/cm³</param>
    /// <param name="materialCost">In USD</param>
    /// <returns>Cost in USD</returns>
    public decimal CalculateCost(decimal materialDensity, decimal materialCost)
        => (CalculateWeight(materialDensity) / 1000) // weight in kg
            * materialCost
            * CustomizationConstants.ProfitMargin;
}
