﻿using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Account.Domain.Roles.DomainEvents;

public record RoleEditedDomainEvent(
    RoleId Id,
    Role Role
) : BaseDomainEvent;
