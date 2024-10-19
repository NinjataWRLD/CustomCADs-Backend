using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Catalog.Persistence.Repositories;

public static class Utilities
{
    public static IQueryable<TEntity> WithTracking<TEntity>(
        this DbSet<TEntity> entities,
        bool track) where TEntity : class
        => track
            ? entities
            : entities.AsNoTracking();

    public static IQueryable<Product> WithFilter(this IQueryable<Product> query, Guid? creatorId = null, string? status = null)
    {
        if (creatorId != null)
        {
            query = query.Where(c => c.CreatorId == creatorId);
        }
        if (status != null && Enum.TryParse(status, ignoreCase: true, out ProductStatus cadStatus))
        {
            query = query.Where(c => c.Status == cadStatus);
        }

        return query;
    }

    public static IQueryable<Product> WithSearch(this IQueryable<Product> query, string? category = null, string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(c => c.Category.Name == category);
        }
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(c => c.Name.Contains(name));
        }

        return query;
    }


    public static IQueryable<Product> WithSorting(this IQueryable<Product> query, string sorting = "")
    {
        return sorting.Capitalize() switch
        {
            nameof(ProductSorting.Newest) => query.OrderByDescending(c => c.UploadDate),
            nameof(ProductSorting.Oldest) => query.OrderBy(c => c.UploadDate),
            nameof(ProductSorting.Alphabetical) => query.OrderBy(c => c.Name),
            nameof(ProductSorting.Unalphabetical) => query.OrderByDescending(c => c.Name),
            nameof(ProductSorting.Category) => query.OrderBy(m => m.Category.Name),
            nameof(ProductSorting.ReverseCategory) => query.OrderByDescending(m => m.Category.Name),
            nameof(ProductSorting.Status) => query.OrderBy(m => (int)m.Status),
            nameof(ProductSorting.ReverseStatus) => query.OrderByDescending(m => (int)m.Status),
            nameof(ProductSorting.Cost) => query.OrderBy(m => m.Cost),
            nameof(ProductSorting.ReverseCost) => query.OrderByDescending(m => m.Cost),
            _ => query,
        };
    }

    public static IQueryable<Product> WithPagination(this IQueryable<Product> query, int page = 1, int limit = 20)
    {
        return query.Skip((page + 1) * limit).Take(limit);
    }

    public static string Capitalize(this string original)
    {
        if (original.Length > 1)
        {
            return char.ToUpper(original[0]) + original[1..];
        }

        if (original.Length == 1)
        {
            return char.ToUpper(original[0]).ToString();
        }

        return string.Empty;
    }
}
