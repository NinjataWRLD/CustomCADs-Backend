﻿namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Stats;

public sealed record GetCustomsStatsResponse(
	int PendingCount,
	int AcceptedCount,
	int BegunCount,
	int FinishedCount,
	int CompletedCount,
	int ReportedCount
);
