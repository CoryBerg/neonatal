using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIButtonLogger : MonoBehaviour 
{
	void Start() {
		if(UILogger.UILog.ContainsKey(this.name) == false) {
			UILogger.UILog.Add(this.name,0);
		}
	}

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		if(UILogger.UILog.ContainsKey(this.name)) {
			UILogger.UILog[this.name]++;
		} else {
			UILogger.UILog.Add(this.name,1);
		}
		UILogger.ButtonsPressed.Add(this.name);
	}
}
