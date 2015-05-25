using UnityEngine;
using System.Collections;

public enum NeonatalCase {
	Cardiac,
	Respiratory,
    Tutorial
}

public class CaseHandler : Singleton<CaseHandler> {
	public NeonatalCase currentCase;
	public bool babyAlive;

	// Use this for initialization
	protected override void Awake () {
		base.Awake();

        Application.runInBackground = true;
		babyAlive = true;
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

	public void ActivateRespiratory() {
		ActivateCase(NeonatalCase.Respiratory);
	}
	
	public void ActivateCardiac() {
		ActivateCase(NeonatalCase.Cardiac);
	}

    public void ActivateTutorial() {
        ActivateCase(NeonatalCase.Tutorial);
    }
}
