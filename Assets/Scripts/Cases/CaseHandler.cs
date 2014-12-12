using UnityEngine;
using System.Collections;

public enum NeonatalCase {
	Cardiac,
	Respiratory
}

public class CaseHandler : MonoBehaviour {
	public static CaseHandler Instance;
	public NeonatalCase currentCase;
	public bool babyAlive;
	// Use this for initialization
	void Awake () {
		if(Instance != null) {
			Destroy(this.gameObject);
			return;
		}
        Application.runInBackground = true;
		babyAlive = true;
		DontDestroyOnLoad(this.gameObject);
		Instance = this;
	}

	public void KillBaby() {
		babyAlive = false;
	}

	public void ReviveBaby() {
		babyAlive = true;
	}

	public void ActivateCase(NeonatalCase aCase) {
		ReviveBaby();
		currentCase = aCase;
	}

	public void ActivateNext() {
		if(currentCase == NeonatalCase.Cardiac) {
			currentCase = NeonatalCase.Respiratory;
		} else {
			currentCase = NeonatalCase.Cardiac;
		}
	}

	public void ActivateCardiac() {
		ActivateCase(NeonatalCase.Cardiac);
	}

	public void ActivateRespiratory() {
		ActivateCase(NeonatalCase.Respiratory);
	}
}
