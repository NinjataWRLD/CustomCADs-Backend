using CustomCADs.Customizations.Domain.Common.Exceptions.Customizations;

namespace CustomCADs.Customizations.Domain.Customizations.Validation;

using static CustomizationConstants;

public static class CustomizationValidation
{
    public static Customization ValidateScale(this Customization customization)
    {
        string property = "Scale";
        decimal scale = customization.Scale;

        if (scale < ScaleMin || scale > ScaleMax)
        {
            throw CustomizationValidationException.Range(property, ScaleMin, ScaleMax);
        }

        return customization;
    }

    public static Customization ValidateInfill(this Customization customization)
    {
        string property = "Infill";
        decimal infill = customization.Infill;

        if (infill < InfillMin || infill > InfillMax)
        {
            throw CustomizationValidationException.Range(property, InfillMin, InfillMax);
        }

        return customization;
    }
    
    public static Customization ValidateVolume(this Customization customization)
    {
        string property = "Volume";
        decimal volume = customization.Volume;

        if (volume < VolumeMin)
        {
            throw CustomizationValidationException.Min(property, VolumeMin);
        }

        return customization;
    }
    
    public static Customization ValidateColor(this Customization customization)
    {
        string property = "Color";
        string color = customization.Color;

        if (string.IsNullOrEmpty(color))
        {
            throw CustomizationValidationException.NotNull(property);
        }

        if (!Color.IsMatch(color))
        {
            throw CustomizationValidationException.Color();
        }

        return customization;
    }
}
