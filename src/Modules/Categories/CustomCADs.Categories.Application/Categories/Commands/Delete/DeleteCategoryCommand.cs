﻿namespace CustomCADs.Categories.Application.Categories.Commands.Delete;

public sealed record DeleteCategoryCommand(
    CategoryId Id
) : ICommand;
