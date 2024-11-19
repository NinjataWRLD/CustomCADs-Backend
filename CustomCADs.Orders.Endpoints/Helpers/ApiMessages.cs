namespace CustomCADs.Orders.Endpoints.Helpers;

public static class ApiMessages
{
    public const string ForbiddenAccess = "Not allowed to modify another User's resources.";
    public const string InvalidOrderStatus = "Order Status must be a value from [{0}].";
    public const string InvalidDeliveryType = "Delivery Type must be a value from [{0}].";
}
