using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UILogger : MonoBehaviour {
	public dfLabel textbox;
	public static Dictionary<string,int> UILog; // Mapped button to count of times pressed
	public static List<string> ButtonsPressed; // Order in which buttons are pressed... for evaluation log
	// Use this for initialization
	void Start () {
//		dfButton buttons = gameObject.GetComponentsInChildren<
		UILog = new Dictionary<string, int>();
		ButtonsPressed = new List<string>();
		foreach(dfButton button in gameObject.GetComponentsInChildren(typeof(dfButton))) {
			button.gameObject.AddComponent<UIButtonLogger>();
		}
	}

	public static string ToCSV() {
		string csvString = "";
		foreach(string key in UILog.Keys) {
			int val = UILog[key];
			csvString += string.Format("{0},{1},",key,val.ToString());
		}
		return csvString;
	}

	public void UpdateCSV() {
		// Hiding this for now
		// textbox.Text = ToCSV();
	}

	public void CopyText() {
		TextEditor te = new TextEditor();
		te.content = new GUIContent(ToCSV());
		te.SelectAll();
		te.Copy();
	}
	// Update is called once per frame
	void Update () {
	}
}
