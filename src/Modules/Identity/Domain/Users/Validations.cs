namespace CustomCADs.Identity.Domain.Users;

using static UserConstants;

public static class Validations
{
	public static User ValidateRole(this User user)
		=> user
			.ThrowIfNull(
				expression: (x) => x.Role,
				predicate: string.IsNullOrWhiteSpace
			);

	public static User ValidateUsername(this User user)
		=> user
			.ThrowIfNull(
				expression: (x) => x.Username,
				predicate: string.IsNullOrWhiteSpace
			).ThrowIfInvalidLength(
				expression: (x) => x.Username,
				length: (UsernameMinLength, UsernameMaxLength)
			);

	public static User ValidateEmail(this User user)
		=> user
			.ThrowIfNull(
				expression: (x) => x.Email.Value,
				predicate: string.IsNullOrWhiteSpace
			).ThrowIfPredicateIsFalse(
				expression: (x) => x.Email.Value,
				predicate: Shared.Domain.Constants.Regexes.Email.IsMatch,
				message: "A {0} must have a proper {1}."
			);
}
