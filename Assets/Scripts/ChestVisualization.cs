using UnityEngine;
using System.Collections;

public class ChestVisualization : MonoBehaviour {
	public Camera mainCamera;
	public Camera chestCamera;
	
	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
		chestCamera.gameObject.SetActive(true);
		mainCamera.enabled = false;
	}

	public void GoBack() {
		chestCamera.gameObject.SetActive(false);
		mainCamera.enabled = true;
	}
}
