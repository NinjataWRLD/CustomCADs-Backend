using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Enums;

namespace CustomCADs.Account.Persistence.Repositories.Reads;

public static class Utilities
{
    public static IQueryable<User> WithFilter(this IQueryable<User> query, Guid[]? Ids, string? role = null)
    {
        if (!string.IsNullOrEmpty(role))
        {
            query = query.Where(u => u.RoleName == role);
        }
        
        if (Ids != null && Ids.Length > 0)
        {
            query = query.Where(u => Ids.Contains(u.Id));
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
            query = query.Where(u => u.NameInfo.FirstName != null && u.NameInfo.FirstName.Contains(firstName));
        }
        if (!string.IsNullOrWhiteSpace(lastName))
        {
            query = query.Where(u => u.NameInfo.LastName != null && u.NameInfo.LastName.Contains(lastName));
        }

        return query;
    }

    public static IQueryable<User> WithSorting(this IQueryable<User> query, string sorting = "")
        => sorting switch
        {
            nameof(UserSorting.Username) => query.OrderBy(u => u.Username),
            nameof(UserSorting.ReverseUsername) => query.OrderByDescending(u => u.Username),
            nameof(UserSorting.Email) => query.OrderBy(u => u.Email),
            nameof(UserSorting.ReverseEmail) => query.OrderByDescending(u => u.Email),
            nameof(UserSorting.Role) => query.OrderBy(u => u.RoleName),
            nameof(UserSorting.ReverseRole) => query.OrderByDescending(u => u.RoleName),
            _ => query,
        };

    public static IQueryable<User> WithPagination(this IQueryable<User> query, int page = 1, int limit = 20)
        => query.Skip((page - 1) * limit).Take(limit);
}
