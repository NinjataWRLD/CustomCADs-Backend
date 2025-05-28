namespace CustomCADs.Shared.Infrastructure.Payment;

public static class Messages
{
	public const string SuccessfulPayment = "Payment was successful.";
	public const string CanceledPayment = "Payment was canceled. Please try again or use a different payment method.";
	public const string ProcessingPayment = "Payment is processing. Please wait for confirmation.";
	public const string SuccessfulPaymentCapture = "Payment captured successfully.";
	public const string FailedPaymentCapture = "Failed to capture payment.";
	public const string FailedPaymentMethod = "Payment failed. Please try another payment method.";
	public const string FailedPayment = "Payment requires further action.";
	public const string UnhandledPayment = "Unhandled payment status: {0}";
}
