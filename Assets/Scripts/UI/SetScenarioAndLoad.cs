using UnityEngine;
using System.Collections;

// Attach to a button and load selected scenario
public class SetScenarioAndLoad : MonoBehaviour {
	public NeonatalCase CaseType;
	public bool loadNext = false; // load the next scenario instead of the one specified.

	public void SetScenario() {
		if(loadNext) {
			CaseHandler.Instance.ActivateNext();
		} else {
			CaseHandler.Instance.ActivateCase(CaseType);
		}

		Application.LoadLevel("IntroCase");
	}
}
