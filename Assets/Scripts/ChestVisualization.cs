using UnityEngine;
using System.Collections;

public class ChestVisualization : MonoBehaviour {
	public Camera mainCamera;
	public Camera blipCamera;
	public Camera chestCamera;
	
	// Use this for initialization
	void Start () {

	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
		chestCamera.gameObject.SetActive(true);
		mainCamera.enabled = false;
		blipCamera.enabled = false;
	}

	public void GoBack() {
		chestCamera.gameObject.SetActive(false);
		mainCamera.enabled = true;
		blipCamera.enabled = true;

	}
}
