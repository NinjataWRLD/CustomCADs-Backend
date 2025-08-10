using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;
using System.Text.RegularExpressions;

namespace CustomCADs.Shared.Domain;

public static partial class Constants
{
	public partial class Regexes
	{
		public static Regex Email => EmailRegex();
		public static Regex Phone => PhoneRegex();

		[GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled)]
		private static partial Regex EmailRegex();

		[GeneratedRegex(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled)]
		private static partial Regex PhoneRegex();
	}

	public static class FluentMessages
	{
		public const string RequiredError = "{PropertyName} is required";
		public const string LengthError = "{PropertyName} length must be between {MinLength} and {MaxLength} characters";
		public const string MinimumError = "{PropertyName} length must be more than {MinLength} characters";
		public const string RangeError = "{PropertyName} must be between {From} and {To}";
		public const string EmailError = "Invalid Email";
		public const string PhoneError = "Invalid Phone";
	}

	public static class Cads
	{
		public const string StlContentType = "model/stl";
		public const string GlbContentType = "model/gltf-binary";
		public const string GltfContentType = "model/gltf+json";

		public static readonly string[] PrintableContentTypes = [
			StlContentType,
		];
	}

	public static class Tags
	{
		public static readonly TagId NewId = TagId.New("6813c4b9-bcde-4f95-a1ce-8e545756c8a4");
		public static readonly TagId ProfessionalId = TagId.New("e67f88d5-330a-414d-b45d-32c6806725ab");
		public static readonly TagId PrintableId = TagId.New("38deab9b-8791-4147-9958-64e9f7ec6d78");
		public static readonly TagId PopularId = TagId.New("9a35cbea-806c-4561-ae71-bb21824f2432");

		public const string New = "New";
		public const string Professional = "Professional";
		public const string Printable = "Printable";
		public const string Popular = "Popular";
	}

	public static class Textures
	{
		public static readonly ImageId PLA = ImageId.New(Guid.Parse("9a35cbea-806c-4561-ae71-bb21824f2432"));
		public static readonly ImageId ABS = ImageId.New(Guid.Parse("bed27a31-107a-4b3f-a50a-cb9cc6f376f1"));
		public static readonly ImageId GlowInDark = ImageId.New(Guid.Parse("190a69a3-1b02-43f0-a4f9-cab22826abf3"));
		public static readonly ImageId TUF = ImageId.New(Guid.Parse("38deab9b-8791-4147-9958-64e9f7ec6d78"));
		public static readonly ImageId Wood = ImageId.New(Guid.Parse("3fe2472c-d2c6-434c-a013-ef117319bed3"));
	}

	public static class Roles
	{
		public const string CustomerId = "762ddec2-25c9-4183-9891-72a19d84a839";
		public const string ContributorId = "e1101e2c-32cc-456f-9c82-4f1d1a65d141";
		public const string DesignerId = "f3ad41d3-ee90-4988-9195-8b2a8f4f2733";
		public const string AdminId = "fad1b19d-5333-4633-bd84-d67c64649f65";

		public const string Customer = "Customer";
		public const string Contributor = "Contributor";
		public const string Designer = "Designer";
		public const string Admin = "Administrator";

		public const string CustomerDescription = "Can buy Products from the Gallery as Cart Items; Can request Customs from our Designers and contact them; Can download purchased CADs and track requested Shipments.";
		public const string ContributorDescription = "Can upload 3D Models to the Gallery as Products; Can sell CADs to our Designers and contact them; Can apply to become a Designer himself.";
		public const string DesignerDescription = "Can accept and work on Customers' Customs; Can validate or report Contributors' Products; Can do everything a Contributor can do.";
		public const string AdminDescription = "Can access all non-sensitive info from all resources; Can ban reported resources - Customs, Products, Users, ...; Can modify Categories and Roles.";
	}

	public static class Users
	{
		public const string CustomerUserId = "e38c495f-b1f3-4226-d289-08dd11623eb9";
		public const string ContributorUserId = "af840410-f3f2-4a3b-d28a-08dd11623eb9";
		public const string DesignerUserId = "4337a774-2c5c-4c27-d28b-08dd11623eb9";
		public const string AdminUserId = "cb7749fb-3fff-4902-d28c-08dd11623eb9";

		public const string CustomerAccountId = "2da61b05-1a27-4af9-9df2-be4f1f4e835f";
		public const string ContributorAccountId = "6d963818-23dc-4e9a-aaa8-b4c77252bc97";
		public const string DesignerAccountId = "0fb3212f-7d51-4586-8fc2-0f333ec9fbc1";
		public const string AdminAccountId = "e995039c-a535-4f20-8288-7aadcb71b252";

		public const string CustomerUsername = "For7a7a";
		public const string ContributorUsername = "PDMatsaliev20";
		public const string DesignerUsername = "Oracle3000";
		public const string AdminUsername = "NinjataBG";

		public const string CustomerEmail = "ivanzlatinov006@gmail.com";
		public const string ContributorEmail = "PDMatsaliev20@codingburgas.bg";
		public const string DesignerEmail = "boriskolev2006@gmail.com";
		public const string AdminEmail = "ivanangelov414@gmail.com";
	}

	public static class Tokens
	{
		public const int JwtDurationInMinutes = 15;
		public const int RtDurationInDays = 7;
		public const int LongerRtDurationInDays = 15;
	}
}
