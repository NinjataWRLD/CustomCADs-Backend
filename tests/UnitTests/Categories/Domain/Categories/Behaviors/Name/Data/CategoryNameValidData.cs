﻿namespace CustomCADs.UnitTests.Categories.Domain.Categories.Behaviors.Name.Data;

using static CategoriesData;

public class CategoryNameValidData : CategoryNameData
{
	public CategoryNameValidData()
	{
		Add(ValidName);
		Add(MinValidName);
		Add(MaxValidName);
	}
}
