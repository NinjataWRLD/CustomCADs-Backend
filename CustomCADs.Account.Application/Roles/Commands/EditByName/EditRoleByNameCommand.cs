﻿using CustomCADs.Account.Application.Roles.Commands;

namespace CustomCADs.Account.Application.Roles.Commands.EditByName;

public record EditRoleByNameCommand(string Name, RoleWriteDto Dto);