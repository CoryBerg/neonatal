using UnityEngine;
using System.Collections;

public class SuccessRestartButton : MonoBehaviour {
	private static int visitCount = 0;
	private dfButton button;
	// Use this for initialization
	void Start () {
		button = this.GetComponent<dfButton>();
		if(visitCount >= 1) {
			button.Text = "Restart Scenario";
		} else {
			button.Text = "Next Scenario";
		}
	}
	
	// Update is called once per frame
	void OnClick () {
		if(visitCount >= 1) {
			CaseHandler.Instance.ActivateRespiratory();
			visitCount = 0;
		} else {
			visitCount++;
		}
	}
}
