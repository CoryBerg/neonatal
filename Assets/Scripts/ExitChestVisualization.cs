using UnityEngine;
using System.Collections;

public class ExitChestVisualization : MonoBehaviour {
	public Camera mainCamera;
	public Camera chestCamera;
	
	// Update is called once per frame
	void Update () {
		if (mainCamera.enabled == false) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				mainCamera.enabled = true;
				chestCamera.enabled = false;
			}
		}
	}
}
