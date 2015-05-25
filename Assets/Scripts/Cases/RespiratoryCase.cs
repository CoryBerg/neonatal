using UnityEngine;
using System.Collections;

// Also acting as parent class for all cases
public class RespiratoryCase : Case {

	protected override void Awake() {
		babyBreath = BabyAnimatorController.Instance.gameObject.GetComponent<Breathing>();
		babyBody = GameObject.FindGameObjectWithTag("BabyBody");
		babyMaterial = babyBody.renderer.material;
		heartMonitor = GameObject.Find("HeartMonitor").GetComponent<SWP_HeartRateMonitor>();

		InitialState ();
		decompTimer = 600f;
		deathTimer = 900f;

        StartCoroutine(LipsOn(3f));
		
		babyEtt = ArmAnimatorController.Instance.babyEtt;

		EttPositioning ();
	}
	
	// Initial state of baby
	protected override void InitialState() {
		Sp02 = "75%";
		temperature = "37.1";
		babyBreath.respRate = 90f;
		bloodPressure = "40/25";
		heartRate = "180";
		bpm = 180;

		// Right lung not working
		babyBreath.both = false;

		babyBody = GameObject.FindGameObjectWithTag("BabyBody");
        babyMaterial = babyBody.renderer.material;
		
		UpdateMonitor();
	}

	// Needle decomp by 5 min in correct location
	protected override void BabyRecovery() {
		currentState = 2;
		
		Sp02 = "94%";
		babyBreath.respRate = 40f;
		bloodPressure = "65/35";
		bpm = 140;
		heartRate = "140";

		// Both lungs working
		babyBreath.both = true;

        Invoke("ChangeScene", 37.0f);

        StartCoroutine(LipsOff(15f));
        BabyAnimatorController.Instance.SetRecovery();
        StartCoroutine(updateNeedleDecompAfterDelay());
	}

	// No needle decomp by 5 min (regardless of interations or lack thereof) or needle decomp in incorrect location
	protected override void FurtherDecomp() {
		Debug.Log ("Enter FurtherDecomp");
		currentState = 1;
		
		Sp02 = "60%";
		babyBreath.respRate = 120f;
		bloodPressure = "35/15";
		bpm = 220;
		heartRate = "220";
		
		StartCoroutine(LipsOn(3f));
		
		UpdateMonitor();
	}

	// No needle decomp by 10 min (5+5, regardless of interations or lack thereof) or needle decomp in incorrect location
	protected override void BabyDeath() {
		currentState = 3;
		
		Sp02 = "30%";
		babyBreath.respRate = 0f;
		bloodPressure = "15/5";
		bpm = 0;
		heartRate = "0";

		// END SCENARIO WITH FAIL
        Invoke("ChangeScene", 15.0f);

        StartCoroutine(LipsOn(3f));
		UpdateMonitor();
	}
}
