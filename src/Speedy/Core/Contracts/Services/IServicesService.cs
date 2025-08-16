using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Models.Calculation.Recipient;
using CustomCADs.Speedy.Core.Services.Models.Calculation.Sender;
using CustomCADs.Speedy.Core.Services.Services.Models;

namespace CustomCADs.Speedy.Core.Contracts.Services;

internal interface IServicesService
{
	Task<(string Deadline, CourierServiceModel CourierService)[]> DestinationServices(AccountModel account, CalculationRecipientModel recipient, DateOnly? date = null, CalculationSenderModel? sender = null, CancellationToken ct = default);
	Task<CourierServiceModel[]> Services(AccountModel account, DateOnly? date = null, CancellationToken ct = default);
}
