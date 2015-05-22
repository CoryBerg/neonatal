using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIButtonLoggerNew : MonoBehaviour 
{
	void Start() {
		this.gameObject.GetComponent<Button>().onClick.AddListener(() => this.gameObject.GetComponent<Button>().gameObject.GetComponent<UIButtonLoggerNew>().LogButton());

		if(UILoggerNew.UILog.ContainsKey(this.name) == false) {
			UILoggerNew.UILog.Add(this.name, 0);
		}
	}
	
	public void LogButton()
	{
		if(UILoggerNew.UILog.ContainsKey(this.name)) {
			Debug.Log("Hit " + this.name + " " + UILoggerNew.UILog[this.name] + " times");
			UILoggerNew.UILog[this.name]++;
		} else {
			Debug.Log("Adding " + this.name + " to log");
			UILoggerNew.UILog.Add(this.name, 1);
		}

		string trimmedButton = this.name.TrimEnd("Button".ToCharArray());
		trimmedButton = trimmedButton.Trim();

		UILoggerNew.ButtonsPressed.Add(trimmedButton);
	}
}
