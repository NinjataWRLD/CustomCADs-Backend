﻿namespace CustomCADs.Shared.Speedy.Services.CalculationService.Calculation;

using Dtos.CalculationContent;
using Dtos.CalculationRecipient;
using Dtos.CalculationSender;
using Dtos.CalculationService;
using Dtos.ShipmentPayment;

public record CalculationRequest(
    string UserName,
    string Password,
    CalculationRecipientDto Recipient,
    CalculationServiceDto Service,
    CalculationContentDto Content,
    ShipmentPaymentDto Payment,
    CalculationSenderDto? Sender,
    string? Location,
    long? ClientSystemId
);