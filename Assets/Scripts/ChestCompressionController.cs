using UnityEngine;
using System.Collections;

public class ChestCompressionController : MonoBehaviour {
	public dfPanel mainPanel;

	public void HideMainButtons() {
		ButtonChange(false);
	}

	void ButtonChange(bool on) {
		mainPanel.IsVisible = on;
	}

	public void BeginCompression() {
		// starts compression or continues if it's already going
		ArmAnimatorController.Instance.TriggerAnimation ("StartCC");
	}

	public void StopCompression() {
		// ends compression, closes controls
		ArmAnimatorController.Instance.TriggerAnimation ("EndCC");
		ButtonChange(true);
	}
}
