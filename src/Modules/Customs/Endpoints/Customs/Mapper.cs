using CustomCADs.Customs.Application.Customs.Dtos;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Endpoints.Customs.Dtos;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.CalculateShipment;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Recent;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Single;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Create;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.Single;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Finish;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs;

using CustomerGetCustomsRespose = Endpoints.Customers.Get.All.GetCustomsResponse;
using DesignerGetCustomsRespose = Endpoints.Designer.Get.All.GetCustomsResponse;

internal static class Mapper
{
	internal static CustomerGetCustomsRespose ToGetResponse(this GetAllCustomsDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus.ToString()
		);

	internal static RecentCustomsResponse ToRecentResponse(this GetAllCustomsDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			OrderedAt: custom.OrderedAt,
			DesignerName: custom.DesignerName
		);

	internal static PostCustomResponse ToPostResponse(this CustomerGetCustomByIdDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus.ToString()
		);

	internal static CalculateCustomShipmentResponse ToResponse(this CalculateShipmentDto calculation)
		=> new(
			Service: calculation.Service,
			Total: calculation.Total,
			Currency: calculation.Currency,
			PickupDate: calculation.PickupDate,
			DeliveryDeadline: calculation.DeliveryDeadline
		);

	internal static GetCustomResponse ToResponse(this CustomerGetCustomByIdDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus.ToString(),
			AcceptedCustom: custom.AcceptedCustom?.ToResponse(),
			FinishedCustom: custom.FinishedCustom?.ToResponse(),
			CompletedCustom: custom.CompletedCustom?.ToResponse()
		);

	internal static DesignerGetCustomResponse ToResponse(this DesignerGetCustomByIdDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus.ToString(),
			BuyerName: custom.BuyerName,
			AcceptedCustom: custom.AcceptedCustom?.ToResponse(),
			FinishedCustom: custom.FinishedCustom?.ToResponse(),
			CompletedCustom: custom.CompletedCustom?.ToResponse()
		);

	internal static DesignerGetCustomsRespose ToResponse(this GetAllCustomsDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			BuyerName: custom.BuyerName
		);

	internal static (string Key, string ContentType, decimal Volume) ToTuple(this FinishCustomRequest req)
		=> (Key: req.CadKey, ContentType: req.CadContentType, Volume: req.CadVolume);

	internal static PaymentResponse ToResponse(this PaymentDto payment)
		=> new(
			ClientSecret: payment.ClientSecret,
			Message: payment.Message
		);

	internal static AcceptedCustomResponse ToResponse(this AcceptedCustomDto custom)
		=> new(
			DesignerName: custom.DesignerName,
			AcceptedAt: custom.AcceptedAt
		);

	internal static FinishedCustomResponse ToResponse(this FinishedCustomDto custom)
		=> new(
			Price: custom.Price,
			FinishedAt: custom.FinishedAt,
			CadId: custom.CadId.Value
		);

	internal static CompletedCustomResponse ToResponse(this CompletedCustomDto custom)
		=> new(
			CustomizationId: custom.CustomizationId?.Value,
			ShipmentId: custom.ShipmentId?.Value
		);
}
