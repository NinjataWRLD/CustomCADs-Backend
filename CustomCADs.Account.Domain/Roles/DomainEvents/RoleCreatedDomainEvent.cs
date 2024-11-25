﻿using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleCreatedDomainEvent(
    Role Role
) : BaseDomainEvent;
