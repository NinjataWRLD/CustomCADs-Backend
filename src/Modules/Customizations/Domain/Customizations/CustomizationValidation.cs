﻿using CustomCADs.Customizations.Domain.Customizations.Validation;

namespace CustomCADs.Customizations.Domain.Customizations;

using static CustomizationConstants;

public static class CustomizationValidation
{
	public static Customization ValidateScale(this Customization customization)
	{
		string property = "Scale";
		decimal scale = customization.Scale;

		if (scale < ScaleMin || scale > ScaleMax)
		{
			throw CustomValidationException<Customization>.Range(property, ScaleMin, ScaleMax);
		}

		return customization;
	}

	public static Customization ValidateInfill(this Customization customization)
	{
		string property = "Infill";
		decimal infill = customization.Infill;

		if (infill < InfillMin || infill > InfillMax)
		{
			throw CustomValidationException<Customization>.Range(property, ScaleMin, ScaleMax);
		}

		return customization;
	}

	public static Customization ValidateVolume(this Customization customization)
	{
		string property = "Volume";
		decimal volume = customization.Volume;

		if (volume < VolumeMin)
		{
			throw CustomValidationException<Customization>.Range(property, ScaleMin, ScaleMax);
		}

		return customization;
	}

	public static Customization ValidateColor(this Customization customization)
	{
		string property = "Color";
		string color = customization.Color;

		if (string.IsNullOrEmpty(color))
		{
			throw CustomValidationException<Customization>.NotNull(property);
		}

		if (!Color.IsMatch(color))
		{
			throw CustomValidationException<Customization>.Custom("A Customization must have a proper color.");
		}

		return customization;
	}
}
