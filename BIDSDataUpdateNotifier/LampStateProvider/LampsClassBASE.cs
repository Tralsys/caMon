using System.IO;
using System.Xml.Serialization;

namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public abstract class LampsClassBASE<IndexClassType>
	{
		protected readonly string SettingXMLFileName = typeof(IndexClassType).FullName + ".xml";
		static private readonly XmlSerializer IndexXmlSerializer = new(typeof(IndexClassType));

		public abstract IndexClassType LampsIndexes { get; set; }

		public void LoadIndexesFromXML()
		{
			using StreamReader sr = new(Path.Combine(ConstValues.DllDirectory, SettingXMLFileName));
			if (IndexXmlSerializer.Deserialize(sr) is IndexClassType indexes)
				LampsIndexes = indexes;
		}
		public async void SaveIndexesToXML()
		{
			using StreamWriter sw = new(Path.Combine(ConstValues.DllDirectory, SettingXMLFileName));
			IndexXmlSerializer.Serialize(sw, LampsIndexes);
			await sw.FlushAsync();
		}

	}
}
