﻿namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;

public record GetAllAccountsDto(
	AccountId Id,
	string Username,
	string Email,
	string Role,
	string? FirstName,
	string? LastName,
	DateTimeOffset CreatedAt
);
