using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Contracts.Dtos;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Speedy.Http.Enums;
using CustomCADs.Speedy.Core.Services;
using CustomCADs.Speedy.Core.Services.Calculation;
using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Print;
using CustomCADs.Speedy.Core.Services.Shipment;
using CustomCADs.Speedy.Core.Services.Track;
using Microsoft.Extensions.Options;

namespace CustomCADs.Delivery.Infrastructure;

internal sealed class SpeedyService(
	IOptions<DeliverySettings> settings,
	ShipmentService shipmentService,
	CalculationService calculationService,
	PrintService printService,
	TrackService trackService
) : IDeliveryService
{
	private readonly AccountModel account = new(settings.Value.Username, settings.Value.Password);
	private const Payer payer = Payer.RECIPIENT;
	private const PaperSize paper = PaperSize.A4;

	public async Task<CalculationDto[]> CalculateAsync(CalculateRequest req, CancellationToken ct = default)
	{
		var response = await calculationService.CalculateAsync(
			account: account,
			payer: payer,
			weights: req.Weights,
			country: req.Country,
			site: req.City,
			street: req.Street,
			ct: ct
		).ConfigureAwait(false);

		return [.. response.Select(c => new CalculationDto(
			Service: c.Service,
			Price: new(
				Amount: c.Price.Amount,
				Vat: c.Price.Vat,
				Total: c.Price.Total,
				Currency: c.Price.Currency
			),
			PickupDate: c.PickupDate,
			DeliveryDeadline: c.DeliveryDeadline
		))];
	}

	public async Task<ShipmentDto> ShipAsync(
		ShipRequestDto req,
		CancellationToken ct = default
	)
	{
		var response = await shipmentService.CreateShipmentAsync(
			account: account,
			payer: payer,
			package: req.Package,
			contents: req.Contents,
			parcelCount: req.ParcelCount,
			totalWeight: req.TotalWeight,
			country: req.Country,
			site: req.City,
			street: req.Street,
			name: req.Name,
			service: req.Service,
			email: req.Email,
			phoneNumber: req.Phone,
			ct: ct
		).ConfigureAwait(false);

		return new(
			Id: response.Id,
			ParcelIds: [.. response.Parcels.Select(p => p.Id)],
			Price: Convert.ToDecimal(response.Price.Amount),
			PickupDate: response.PickupDate,
			DeliveryDeadline: response.DeliveryDeadline
		);
	}

	public async Task CancelAsync(string shipmentId, string comment, CancellationToken ct = default)
		=> await shipmentService.CancelShipmentAsync(
			account: account,
			shipmentId: shipmentId,
			comment: comment,
			ct: ct
		).ConfigureAwait(false);

	public async Task<ShipmentStatusDto[]> TrackAsync(string shipmentId, CancellationToken ct = default)
	{
		var response = await trackService.TrackAsync(
			account: account,
			shipmentId: shipmentId,
			ct: ct
		).ConfigureAwait(false);

		return [.. response.Single().Operations.Select(o => new ShipmentStatusDto(
			DateTime: o.DateTime,
			Place: o.Place,
			Message: o.Translate()
		))];
	}

	public async Task<byte[]> PrintAsync(string shipmentId, CancellationToken ct = default)
		=> await printService.PrintAsync(
			account: account,
			paperSize: paper,
			shipmentId: shipmentId,
			ct: ct
		).ConfigureAwait(false);
}
