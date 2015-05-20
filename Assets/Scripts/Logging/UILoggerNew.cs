using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UILoggerNew : Singleton<UILoggerNew> {
	public static Dictionary<string,int> UILog; // Mapped button to count of times pressed
	public static List<string> ButtonsPressed; // Order in which buttons are pressed... for evaluation log

	public Button[] loggableButtons;
	
	// Use this for initialization
	void Start () {
		UILog = new Dictionary<string, int>();
		ButtonsPressed = new List<string>();

		foreach(Button button in loggableButtons) {
			button.gameObject.AddComponent<UIButtonLoggerNew>();
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
	
	public void CopyText() {
		TextEditor te = new TextEditor();
		te.content = new GUIContent(ToCSV());
		te.SelectAll();
		te.Copy();
	}
}
