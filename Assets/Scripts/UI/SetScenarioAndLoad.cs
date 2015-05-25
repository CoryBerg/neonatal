using UnityEngine;
using System.Collections;

// Attach to a button and load selected scenario
public class SetScenarioAndLoad : MonoBehaviour {
	public NeonatalCase CaseType;

	public void SetScenario() {
		CaseHandler.Instance.ActivateCase(CaseType);

		Application.LoadLevel("IntroCase");
	}
}
