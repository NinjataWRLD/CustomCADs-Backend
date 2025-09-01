using System.Xml.Serialization;

namespace CustomCADs.Shared.Infrastructure.Currencies;

public static class Gesmes
{
	private const string Root = "Envelope";
	private const string GesmesNs = "http://www.gesmes.org/xml/2002-08-01";
	private const string EcbNs = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

	[XmlRoot(Root, Namespace = GesmesNs)]
	public class Envelope
	{
		[XmlElement("subject", Namespace = GesmesNs)]
		public required string Subject { get; set; }

		[XmlElement("Sender", Namespace = GesmesNs)]
		public required Sender Sender { get; set; }

		[XmlElement("Cube", Namespace = EcbNs)]
		public required CubeRoot Cube { get; set; }
	}

	public class Sender
	{
		[XmlElement("name", Namespace = GesmesNs)]
		public required string Name { get; set; }
	}

	public class CubeRoot
	{
		[XmlElement("Cube", Namespace = EcbNs)]
		public required CubeTime TimeCube { get; set; }
	}

	public class CubeTime
	{
		[XmlAttribute("time")]
		public required DateTimeOffset Time { get; set; }

		[XmlElement("Cube", Namespace = EcbNs)]
		public required List<CubeRate> Rates { get; set; }
	}

	public class CubeRate
	{
		[XmlAttribute("currency")]
		public required string Currency { get; set; }

		[XmlAttribute("rate")]
		public required decimal Rate { get; set; }
	}
}
