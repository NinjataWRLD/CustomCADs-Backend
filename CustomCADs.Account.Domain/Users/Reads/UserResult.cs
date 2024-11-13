using CustomCADs.Account.Domain.Users.Entities;

namespace CustomCADs.Account.Domain.Users.Reads;

public record UserResult(int Count, ICollection<User> Users);
