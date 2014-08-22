using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

// This is the leaft of the XML tree of a Data Object
public class DataItem {
		[XmlAttribute("name")]
		public string Name;

		[XmlElement("Value")]
		public string Value;

		// If we want XML to be able to specify functionality, data lists should have different flags that would be specified here.
}
