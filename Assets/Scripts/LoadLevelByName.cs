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

}
