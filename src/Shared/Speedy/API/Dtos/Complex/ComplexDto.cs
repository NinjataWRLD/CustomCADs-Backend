﻿namespace CustomCADs.Shared.Speedy.API.Dtos.Complex;

public record ComplexDto(
	long Id,
	long SiteId,
	string Type,
	string TypeEn,
	string Name,
	string NameEn,
	long ActualId,
	string ActualType,
	string ActualTypeEn,
	string ActualName,
	string ActualNameEn
);
