using CustomCADs.Speedy.Core.Models.Calculation.Recipient;
using CustomCADs.Speedy.Core.Models.Calculation.Sender;

namespace CustomCADs.Speedy.Core.Contracts.Services;

internal interface IServicesService
{
	Task<(string Deadline, CourierServiceModel CourierService)[]> DestinationServices(SpeedyAccount account, CalculationRecipientModel recipient, DateOnly? date = null, CalculationSenderModel? sender = null, CancellationToken ct = default);
	Task<CourierServiceModel[]> Services(SpeedyAccount account, DateOnly? date = null, CancellationToken ct = default);
}
