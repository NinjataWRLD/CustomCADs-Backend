
namespace CustomCADs.Shared.Core.Common;

public record Result<TItem>(
    int Count,
    ICollection<TItem> Items
)
{
    public static implicit operator Result<TItem>(CustomCADs.Account.Domain.Users.Reads.UserResult v)
    {
        throw new NotImplementedException();
    }
}
