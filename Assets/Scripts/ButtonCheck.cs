// report what is the current action
// record how many times the same button has been pressed

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class ButtonCheck : MonoBehaviour {

	public bool showCurrentOperation = true;

	private GameObject popup;
	private string action;
	private int clickCount = 0;


	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
//		Debug.Log (this.transform.name);

		if (showCurrentOperation) {
			action = this.GetComponent<dfButton> ().Text;
		}

		clickCount += 1;
		//PlayerPrefs.SetInt(this.transform.name, clickCount);
		UpdateButtonCount (action, clickCount);
//		Debug.Log ( this.transform.name + "has been pressed " + PlayerPrefs.GetInt(this.transform.name) + " times.");
	}

	public void ButtonPressed(GameObject buttonText) {
		if (showCurrentOperation) {
			action = buttonText.GetComponent<Text> ().text;
		}
		
		clickCount += 1;
		UpdateButtonCount (action, clickCount);
	}

	public void UpdateButtonCount(string button, int clickCount)
	{	
		// Load buttonCount if there is one
		// Create a new dictionary if there isn't one
		Dictionary<string,string> buttonCount = new Dictionary<string, string>();

		if (PlayerPrefs.HasKey ("buttonCount")) {
			string buttonCountSerialized = PlayerPrefs.GetString ("buttonCount", "");
			buttonCount = MyUnserialize (buttonCountSerialized);
		}

		//Update buttonCount
		buttonCount[button] = clickCount.ToString ();

		// Save buttonCount
		string buttonCountSerializedAgain = MySerialize(buttonCount);
		PlayerPrefs.SetString ("buttonCount", buttonCountSerializedAgain);
		PlayerPrefs.Save ();
	}

	public Dictionary<string, string> MyUnserialize(string text)
	{
		Dictionary<string, string> dict = new Dictionary<string,string> ();
		string[] tokens = text.Split (new[]{'=',';'}, System.StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i < tokens.Length; i += 2) {
			string name = tokens[i];
			string freq = tokens[i + 1];
			
			dict.Add(name, freq);
		}

		return dict;
	}

	public string MySerialize(Dictionary<string, string> dict)
	{
		StringBuilder builder = new StringBuilder();
		foreach (var entry in dict) {
			builder.Append(entry.Key).Append('=').Append(entry.Value).Append(';');		
		}

		string text = builder.ToString ();

		return text;
	}
}
