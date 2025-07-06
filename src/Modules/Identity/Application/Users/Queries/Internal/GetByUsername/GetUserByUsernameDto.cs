using CustomCADs.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;

public record GetUserByUsernameDto(
	UserId Id,
	string Role,
	string Username,
	string? FirstName,
	string? LastName,
	Email Email,
	DateTimeOffset CreatedAt,
	ProductId[] ViewedProductIds
);
