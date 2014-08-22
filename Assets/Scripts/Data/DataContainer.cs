using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("NeonatalData")]
public class DataContainer {
	[XmlArray("DataItems"), XmlArrayItem("Item")]
	public List<DataItem> DataItems = new List<DataItem>();

	private Dictionary<string,string> dataDict;

	public string Lookup(string key) {
		if(dataDict == null) {
			dataDict = new Dictionary<string, string>();
			foreach(DataItem data in DataItems) {
				dataDict.Add(data.Name,data.Value);
			}
		}

		try {
			return dataDict[key];
		} catch (KeyNotFoundException e) {
			Debug.Log(e.Message);
			return null;
		}
	}
 
	public static DataContainer Load(string path) {
		XmlSerializer serializer = new XmlSerializer(typeof(DataContainer));
		using(FileStream stream = new FileStream(path, FileMode.Open)) {
				return serializer.Deserialize(stream) as DataContainer;
		}
	}

	public static DataContainer LoadFromTextFile(string path) {
		TextAsset textAsset = (TextAsset)Resources.Load (path);
		XmlSerializer serializer = new XmlSerializer(typeof(DataContainer));
		return serializer.Deserialize(new StringReader(textAsset.text)) as DataContainer;
	}

	public void Save(string path) {
		XmlSerializer serializer = new XmlSerializer(typeof(DataContainer));
		using(FileStream stream = new FileStream(path, FileMode.Create)) {
				serializer.Serialize(stream,this);
		}
	}
}
