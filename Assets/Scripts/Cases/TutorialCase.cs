using UnityEngine;
using System.Collections;

public class TutorialCase : Case {

    protected override void Awake() {
        base.Awake();

        decompTimer = 60000f;
        deathTimer = 900000f;

        babyBreath.both = true;
    }

	protected override void InitialState ()	{
		Sp02 = "94%";
		babyBreath.respRate = 40f;
		bloodPressure = "65/35";
		bpm = 140;
		heartRate = "140";

		UpdateMonitor();
	}
	
	protected override void BabyRecovery ()	{
		TestHandler.Instance.Toast("Baby shows sign of Recovery");
		temperature = "37.1";
		currentState = 2; // win
		Sp02 = "85%";
		bloodPressure = "65/35";
		heartRate = "140";
		bpm = 140;
		// strong pulse
		// cyanosis disabled
		Invoke("ChangeScene", 3.0f);
		
		BabyAnimatorController.Instance.SetRecovery();
		StartCoroutine(LipsOff(3f));
		UpdateMonitor();
	}
	
	// After 20 minutes without prostaglandin drip
	protected override void FurtherDecomp () {
		temperature = "37.1";
		currentState = 1;
		Sp02 = "60%";
		// Cyanosis enabledaa
		// resp rate: 120 b/m
		bloodPressure = "35/15";
		heartRate = "220";
		bpm = 220;
		
		StartCoroutine(LipsOn(3f));
		UpdateMonitor();
		// weak pulse strength
	}
	
	
	protected override void BabyDeath () {
		temperature = "37.1";
		currentState = 3; // death
		Sp02 = "85%";
		bloodPressure = "65/35";
		heartRate = "0";
		bpm = 0;
		// Pulse strength strong
		
		StartCoroutine(LipsOn(1f));
		UpdateMonitor();
	}
}
