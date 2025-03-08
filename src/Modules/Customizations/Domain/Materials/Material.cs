using CustomCADs.Customizations.Domain.Materials.Validations;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Customizations.Domain.Materials;

public class Material : BaseAggregateRoot
{
    private Material() { }
    private Material(string name, decimal density, decimal cost, ImageId textureId) : this()
    {
        Name = name;
        Density = density;
        Cost = cost;
        TextureId = textureId;
    }

    public MaterialId Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Measured in g/cm³
    /// </summary>
    public decimal Density { get; set; }

    /// <summary>
    ///     Measured in USD/kg
    /// </summary>
    public decimal Cost { get; set; }

    public ImageId TextureId { get; set; }

    public static Material Create(
        string name,
        decimal density,
        decimal cost,
        ImageId textureId
    ) => new Material(name, density, cost, textureId)
        .ValidateName()
        .ValidateDensity()
        .ValidateCost();

    public static Material CreateWithId(
        MaterialId id,
        string name,
        decimal density,
        decimal cost,
        ImageId textureId
    ) => new Material(name, density, cost, textureId)
    {
        Id = id,
    }
    .ValidateName()
    .ValidateDensity()
    .ValidateCost();

    public Material SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }

    public Material SetDensity(decimal density)
    {
        Density = density;
        this.ValidateDensity();
        return this;
    }

    public Material SetCost(decimal cost)
    {
        Cost = cost;
        this.ValidateCost();
        return this;
    }
}