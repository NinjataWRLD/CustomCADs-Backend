using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Identity.Domain.Users.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;

public class VerificationEmailHandler(IUserManager manager, IEventRaiser raiser)
    : ICommandHandler<VerificationEmailCommand>
{
    public async Task Handle(VerificationEmailCommand req, CancellationToken ct)
    {
        User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ByProp(nameof(User.Username), req.Username);

        string token = await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        await raiser.RaiseDomainEventAsync(new EmailVerificationRequestedDomainEvent(
            Email: user.Email.Value,
            Endpoint: req.GetUri(token)
        )).ConfigureAwait(false);
    }
}
