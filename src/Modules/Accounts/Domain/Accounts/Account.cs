using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Accounts.Domain.Accounts;

public class Account : BaseAggregateRoot
{
	private Account() { }
	private Account(string role, string username, string email, string? firstName, string? lastName) : this()
	{
		TrackViewedProducts = true;
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
	public bool TrackViewedProducts { get; private set; }
	public DateTimeOffset CreatedAt { get; private set; }

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

	public Account SetTrackViewedProducts(bool track)
	{
		TrackViewedProducts = track;
		return this;
	}
}
