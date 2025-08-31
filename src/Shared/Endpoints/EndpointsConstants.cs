namespace CustomCADs.Shared.Endpoints;

public static class EndpointsConstants
{
	public const string RateLimitPolicy = "user-based";

	public static class Paths
	{
		public const string Identity = "identity";
		public const string ProductsGallery = "products/gallery";
		public const string ActiveCarts = "carts/active";
		public const string Customizations = "customizations";
		public const string PurchasedCarts = "carts/purchased";
		public const string Shipments = "shipments";
		public const string CustomsCustomer = "customs/customer";
		public const string ProductsCreator = "products/creator";
		public const string CustomsDesigner = "customs/designer";
		public const string ProductsDesigner = "products/designer";
		public const string Accounts = "accounts";
		public const string Roles = "roles";
		public const string Categories = "categories";
		public const string Materials = "materials";
		public const string Tags = "tags";
	}

	/// <summary>
	/// 	[Key]: Value -> [Relative Path]: Documentation Tag
	/// </summary>
	public static readonly Dictionary<string, string> Tags = new()
	{
		// AllowAnonymous
		[Paths.Identity] = "0.1. Identity",
		[Paths.ProductsGallery] = "0.2. Product Gallery",
		[Paths.ActiveCarts] = "0.3. Active Carts",
		[Paths.Customizations] = "0.4. Customizations",

		// Customers
		[Paths.PurchasedCarts] = "1.1. Purchased Carts",
		[Paths.Shipments] = "1.2. Shipments",
		[Paths.CustomsCustomer] = "1.3. Customs Collection",

		// Contributors
		[Paths.ProductsCreator] = "2.1. Product Collection",

		// Designers
		[Paths.CustomsDesigner] = "3.1. Customs Management",
		[Paths.ProductsDesigner] = "3.2. Product Management",

		// Admins
		[Paths.Accounts] = "4.1. Accounts Dashboard",
		[Paths.Roles] = "4.2. Roles Dashboard",
		[Paths.Categories] = "4.3. Categories Dashboard",
		[Paths.Materials] = "4.4. Materials Dashboard",
		[Paths.Tags] = "4.5. Tags Dashboard",
	};
}
