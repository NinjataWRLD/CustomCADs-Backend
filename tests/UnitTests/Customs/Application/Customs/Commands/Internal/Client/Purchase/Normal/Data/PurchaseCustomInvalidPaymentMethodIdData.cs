namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Purchase.Normal.Data;

public class PurchaseCustomInvalidPaymentMethodIdData : PurchaseCustomData
{
    public PurchaseCustomInvalidPaymentMethodIdData()
    {
        Add(string.Empty);
        Add(null!);
    }
}
