namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.Normal.Data;

public class PurchaseCustomInvalidPaymentMethodIdData : PurchaseCustomData
{
    public PurchaseCustomInvalidPaymentMethodIdData()
    {
        Add(string.Empty);
        Add(null!);
    }
}
