﻿using MediatR;

namespace CustomCADs.Shared.Application.Requests.Queries;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>;
