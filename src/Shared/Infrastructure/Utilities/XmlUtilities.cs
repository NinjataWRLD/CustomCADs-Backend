using System.Xml;
using System.Xml.Serialization;

namespace CustomCADs.Shared.Infrastructure.Utilities;

public static class XmlUtilities
{
	private static XmlSerializer GetXmlSerializer<TDto>() => new(type: typeof(TDto));

	public static TDto DeserializeFromXml<TDto>(this Stream stream) where TDto : class
		=> GetXmlSerializer<TDto>().Deserialize(stream) as TDto
			?? throw new XmlException($"Failed to parse XML to {typeof(TDto).Name}");

	public static void SerializeToXml<TDto>(this TDto dto, Stream stream) where TDto : class
		=> GetXmlSerializer<TDto>().Serialize(stream, dto);

	public static string SerializeToXml<TDto>(this TDto dto) where TDto : class
	{
		System.Text.StringBuilder builder = new();
		using StringWriter writer = new(builder);

		GetXmlSerializer<TDto>().Serialize(writer, dto);
		return builder.ToString();
	}
}
