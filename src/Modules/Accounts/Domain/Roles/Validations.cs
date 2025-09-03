namespace CustomCADs.Accounts.Domain.Roles;

using static RoleConstants;

public static class Validations
{
	public static Role ValidateName(this Role role)
		=> role
			.ThrowIfNull(
				expression: x => x.Name,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: x => x.Name,
				length: (NameMinLength, NameMaxLength)
			);

	public static Role ValidateDescription(this Role role)
		=> role
			.ThrowIfNull(
				expression: x => x.Description,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: x => x.Description,
				length: (DescriptionMinLength, DescriptionMaxLength)
			);
}
