using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace KioskDisplay
{
	public static class XmlHelper
	{
		public static string SerializeToFile<T>(T obj)
			where T : class, new()
		{
			var outputFile = Path.GetTempFileName();

			SerializeToFile(obj, outputFile);

			return outputFile;
		}

		public static void SerializeToFile<T>(T obj, string outputFile)
			where T : class, new()
		{
			if (File.Exists(outputFile))
			{
				File.Delete(outputFile);
			}

			var serializer = new XmlSerializer(typeof(T));
			using (var stream = File.OpenWrite(outputFile))
			{
				using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
				{
					serializer.Serialize(writer, obj);
					writer.Flush();
				}
			}
		}

		public static string Transform(string xmlPath, string xsltUrl)
		{
			var outputFile = Path.GetTempFileName();
			var fileData = string.Empty;

			try
			{
				var transform = new XslCompiledTransform();

				var xsltSettings = new XsltSettings(true, true);
				transform.Load(xsltUrl, xsltSettings, new XmlUrlResolver());

				transform.Transform(xmlPath, outputFile);

				fileData = File.ReadAllText(outputFile);
			}
			finally
			{
				if (File.Exists(outputFile))
					File.Delete(outputFile);
			}

			return fileData;
		}

		public static string Transform<T>(T data, string xsltUrl)
			where T : class, new()
		{
			var xmlPath = SerializeToFile(data);
			return Transform(xmlPath, xsltUrl);
		}

		public static T Deserialize<T>(string outputFilename)
			where T : class, new()
		{
			var serializer = new XmlSerializer(typeof(T));
			using (var fs = File.OpenRead(outputFilename))
			{
				return serializer.Deserialize(fs) as T;
			}
		}
	}
}