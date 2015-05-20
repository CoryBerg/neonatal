using UnityEngine;
using System.Collections;

public class ChestCompressionController : MonoBehaviour {
	public Camera[] cameras;

	public void HideMainButtons() {
		foreach (Camera camera in cameras) {
			camera.animation ["chestCompZoom"].time = 0;
			camera.animation ["chestCompZoom"].speed = 1.0f;
			camera.animation.Play ("chestCompZoom");
		}
	}

	public void BeginCompression() {
		// starts compression or continues if it's already going
		ArmAnimatorController.Instance.TriggerAnimation ("StartCC");
	}

	public void StopCompression() {
		// ends compression, closes controls
		ArmAnimatorController.Instance.TriggerAnimation ("EndCC");
	}
}
