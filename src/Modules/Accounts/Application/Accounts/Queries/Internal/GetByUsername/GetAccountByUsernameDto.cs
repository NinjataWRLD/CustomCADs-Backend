﻿namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetByUsername;

public record GetAccountByUsernameDto(
	AccountId Id,
	string Role,
	string Username,
	string Email,
	string? FirstName,
	string? LastName,
	DateTimeOffset CreatedAt
);
