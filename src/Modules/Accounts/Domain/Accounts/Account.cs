using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Accounts.Domain.Accounts;

public class Account : BaseAggregateRoot
{
	private readonly List<ProductId> viewedProductIds = [];

	private Account() { }
	private Account(string role, string username, string email, string? firstName, string? lastName) : this()
	{
		CreatedAt = DateTimeOffset.UtcNow;
		RoleName = role;
		Username = username;
		Email = email;
		FirstName = firstName;
		LastName = lastName;
	}

	public AccountId Id { get; init; }
	public string Username { get; private set; } = string.Empty;
	public string Email { get; private set; } = string.Empty;
	public string? FirstName { get; private set; }
	public string? LastName { get; private set; }
	public string RoleName { get; private set; } = string.Empty;
	public DateTimeOffset CreatedAt { get; private set; }
	public IReadOnlyCollection<ProductId> ViewedProductIds => viewedProductIds.AsReadOnly();

	public static Account Create(
		string role,
		string username,
		string email,
		string? firstName = default,
		string? lastName = default
	) => new Account(role, username, email, firstName, lastName)
			.ValidateRole()
			.ValidateUsername()
			.ValidateEmail()
			.ValidateFirstName()
			.ValidateLastName();

	public static Account CreateWithId(
		AccountId id,
		string role,
		string username,
		string email,
		DateTimeOffset createdAt,
		string? firstName = default,
		string? lastName = default
	) => new Account(role, username, email, firstName, lastName)
	{
		Id = id,
		CreatedAt = createdAt
	}
	.ValidateRole()
	.ValidateUsername()
	.ValidateEmail()
	.ValidateFirstName()
	.ValidateLastName();

	public Account AddViewedProduct(ProductId id)
	{
		if (!viewedProductIds.Contains(id))
		{
			viewedProductIds.Add(id);
		}

		return this;
	}

	public Account SetUsername(string username)
	{
		Username = username;
		this.ValidateUsername();
		return this;
	}

	public Account SetEmail(string email)
	{
		Email = email;
		this.ValidateEmail();
		return this;
	}

	public Account SetFirstName(string? firstName)
	{
		FirstName = firstName;
		this.ValidateFirstName();
		return this;
	}

	public Account SetLastName(string? lastName)
	{
		LastName = lastName;
		this.ValidateLastName();
		return this;
	}
}
