using CustomCADs.Shared.Speedy.API.Dtos.ShipmentPayment;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;

internal static class Mapper
{
    internal static ShipmentPaymentDto ToDto(this ShipmentPaymentModel model)
        => new(
            CourierServicePayer: model.CourierServicePayer,
            DeclaredValuePayer: model.DeclaredValuePayer,
            PackagePayer: model.PackagePayer,
            ThirdPartyClientId: model.ThirdPartyClientId,
            DiscountCardId: model.DiscountCardId?.ToDto(),
            SenderBankAccount: model.SenderBankAccount?.ToDto(),
            AdministrativeFee: model.AdministrativeFee
        );

    internal static ShipmentDiscountCardIdDto ToDto(this ShipmentDiscountCardIdModel model)
        => new(
            ContractId: model.ContractId,
            CardId: model.CardId
        );

    internal static BankAccountDto ToDto(this BankAccountModel model)
        => new(
            Iban: model.Iban,
            AccountHolder: model.AccountHolder
        );

    internal static ShipmentPaymentModel ToModel(this ShipmentPaymentDto dto)
        => new(
            CourierServicePayer: dto.CourierServicePayer,
            DeclaredValuePayer: dto.DeclaredValuePayer,
            PackagePayer: dto.PackagePayer,
            ThirdPartyClientId: dto.ThirdPartyClientId,
            DiscountCardId: dto.DiscountCardId?.ToModel(),
            SenderBankAccount: dto.SenderBankAccount?.ToModel(),
            AdministrativeFee: dto.AdministrativeFee
        );

    internal static ShipmentDiscountCardIdModel ToModel(this ShipmentDiscountCardIdDto dto)
        => new(
            ContractId: dto.ContractId,
            CardId: dto.CardId
        );

    internal static BankAccountModel ToModel(this BankAccountDto dto)
        => new(
            Iban: dto.Iban,
            AccountHolder: dto.AccountHolder
        );
}
