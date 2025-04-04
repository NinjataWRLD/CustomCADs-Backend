﻿using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Client.GetCadUrlGet;

public sealed record GetCustomCadPresignedUrlGetQuery(
    CustomId Id,
    AccountId BuyerId
) : IQuery<GetCustomCadPresignedUrlGetDto>;
