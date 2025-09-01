using System.Xml.Serialization;

namespace CustomCADs.Shared.Infrastructure.Currencies;

internal static class Gesmes
{
	private const string Root = "Envelope";
	private const string GesmesNs = "http://www.gesmes.org/xml/2002-08-01";
	private const string EcbNs = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

	[XmlRoot(Root, Namespace = GesmesNs)]
	internal class Envelope
	{
		[XmlElement("subject", Namespace = GesmesNs)]
		internal required string Subject { get; set; }

		[XmlElement("Sender", Namespace = GesmesNs)]
		internal required Sender Sender { get; set; }

		[XmlElement("Cube", Namespace = EcbNs)]
		internal required CubeRoot Cube { get; set; }
	}

	internal class Sender
	{
		[XmlElement("name", Namespace = GesmesNs)]
		internal required string Name { get; set; }
	}

	internal class CubeRoot
	{
		[XmlElement("Cube", Namespace = EcbNs)]
		internal required CubeTime TimeCube { get; set; }
	}

	internal class CubeTime
	{
		[XmlAttribute("time")]
		internal required DateTimeOffset Time { get; set; }

		[XmlElement("Cube", Namespace = EcbNs)]
		internal required List<CubeRate> Rates { get; set; }
	}

	internal class CubeRate
	{
		[XmlAttribute("currency")]
		internal required string Currency { get; set; }

		[XmlAttribute("rate")]
		internal required decimal Rate { get; set; }
	}
}
