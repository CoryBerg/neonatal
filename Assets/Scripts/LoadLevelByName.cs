using System;
using UnityEngine;

public class LoadLevelByName : MonoBehaviour
{
	public string LevelName;

	public void LoadLevel() {
		if(!string.IsNullOrEmpty(LevelName)) {
			Application.LoadLevel(LevelName);
		}
	}

	//TODO: Remove this
	void OnClick( dfControl control, dfMouseEventArgs mouseEvent ) {
		if(!string.IsNullOrEmpty(LevelName)) {
			Application.LoadLevel(LevelName);
		}
	}
}
