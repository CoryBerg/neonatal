using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShowFeedback : MonoBehaviour {

	// Use this for initialization
	void Start () {

		string buttonCountSerialized = PlayerPrefs.GetString ("buttonCount", "");
		Dictionary<string, string> buttonCount = MyUnserialize (buttonCountSerialized);


		StringBuilder builder = new StringBuilder();
		builder.AppendLine ("You section record:<br>");

		foreach (var entry in buttonCount) {

			builder.AppendLine(entry.Key + " " + entry.Value + " time(s).<br>");
		}

		string message = builder.ToString ();
		this.GetComponent<dfRichTextLabel>().Text = message;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Dictionary<string, string> MyUnserialize(string text)
	{
		Dictionary<string, string> dict = new Dictionary<string,string> ();
		string[] tokens = text.Split (new[]{'=',';'}, System.StringSplitOptions.RemoveEmptyEntries);
		
		for (int i = 0; i < tokens.Length; i += 2)
		{
			string name = tokens[i];
			string freq = tokens[i + 1];
			
			dict.Add(name, freq);
		}
		return dict;
	}
}
