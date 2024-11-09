﻿using CustomCADs.Account.Domain.Roles;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.DomainEvents.Roles;

public record RoleCreatedDomainEvent(
    Role Role
) : DomainEvent;