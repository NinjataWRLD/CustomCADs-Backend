using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Enums;
using CustomCADs.Account.Domain.Users.ValueObjects;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Account.Persistence.Users.Reads;

public static class Utilities
{
    public static IQueryable<User> WithFilter(this IQueryable<User> query, UserId[]? ids, string? role = null)
    {
        if (!string.IsNullOrEmpty(role))
        {
            query = query.Where(u => u.RoleName == role);
        }
        if (ids is not null)
        {
            query = query.Where(u => ids.Contains(u.Id));
        }

        return query;
    }

    public static IQueryable<User> WithSearch(this IQueryable<User> query, string? username = null, string? email = null, string? firstName = null, string? lastName = null)
    {
        if (!string.IsNullOrWhiteSpace(username))
        {
            query = query.Where(u => u.Username.Contains(username));
        }
        if (!string.IsNullOrWhiteSpace(email))
        {
            query = query.Where(u => u.Email.Contains(email));
        }
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            query = query.Where(u => u.Names.FirstName != null && u.Names.FirstName.Contains(firstName));
        }
        if (!string.IsNullOrWhiteSpace(lastName))
        {
            query = query.Where(u => u.Names.LastName != null && u.Names.LastName.Contains(lastName));
        }

        return query;
    }

    public static IQueryable<User> WithSorting(this IQueryable<User> query, UserSorting? sorting = null)
        => sorting switch
        {
            { Type: UserSortingType.Username, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.Username),
            { Type: UserSortingType.Username, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.Username),
            { Type: UserSortingType.Email, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.Email),
            { Type: UserSortingType.Email, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.Email),
            { Type: UserSortingType.Role, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.RoleName),
            { Type: UserSortingType.Role, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.RoleName),
            _ => query,
        };

    public static IQueryable<User> WithPagination(this IQueryable<User> query, int page = 1, int limit = 20)
        => query.Skip((page - 1) * limit).Take(limit);
}
