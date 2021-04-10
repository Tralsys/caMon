using System.IO;
using System.Xml.Serialization;

namespace BIDSDataUpdateNotifier.LampStateProvider
{
	public interface ILampsClassBASE
	{
		void LoadIndexesFromXML();
		void SaveIndexesToXML();
		void LoadIndexesFromXML(string path);
		void SaveIndexesToXML(string path);
	}
	public abstract class LampsClassBASE<IndexClassType> : ILampsClassBASE
	{
		protected readonly string SettingXMLFileName = typeof(IndexClassType).FullName + ".xml";
		static private readonly XmlSerializer IndexXmlSerializer = new(typeof(IndexClassType));

		public abstract IndexClassType LampsIndexes { get; set; }

		public void LoadIndexesFromXML() => LoadIndexesFromXML(SettingXMLFileName);
		public void SaveIndexesToXML() => SaveIndexesToXML(SettingXMLFileName);

		public void LoadIndexesFromXML(string path)
		{
			using StreamReader sr = new(Path.IsPathRooted(path) ? path : Path.Combine(ConstValues.DllDirectory, path));
			if (IndexXmlSerializer.Deserialize(sr) is IndexClassType indexes)
				LampsIndexes = indexes;
		}
		public async void SaveIndexesToXML(string path)
		{
			using StreamWriter sw = new(Path.IsPathRooted(path) ? path : Path.Combine(ConstValues.DllDirectory, path));
			IndexXmlSerializer.Serialize(sw, LampsIndexes);
			await sw.FlushAsync();
		}
	}
}
