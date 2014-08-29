using UnityEngine;
using System.Collections;

// Also acting as parent class for all cases
public class RespiratoryCase : MonoBehaviour {
	public Breathing babyBreath;
	public bool isCorrect = false;
	public float timer = 0.0f;
	public Transform mouthTarget;
	public int bpm;
	public int currentState = 0;
	public string heartRate, Sp02, bloodPressure, temperature;
	/*
	*	States:
	*		0 - Initial
	*		1 - No action 5 minutes or improper needle decomp
	*		2 - Correct needle decomp, baby healthy
	*		3 - No action 10 minutes, or improper needle decomp x2
	*/

	protected GameObject babyBody;
	protected Material babyMaterial;
	protected float decompTimer, deathTimer;
	protected SWP_HeartRateMonitor heartMonitor;

	private GameObject ett, vt;

	protected virtual void Awake() {
		ett = GameObject.FindGameObjectWithTag ("Arms").GetComponent<ArmAnimatorController> ().ett;
		vt = GameObject.FindGameObjectWithTag ("Arms").GetComponent<ArmAnimatorController> ().vt;
		babyBreath = GameObject.FindGameObjectWithTag ("Baby").GetComponent<Breathing> ();
		babyBody = GameObject.FindGameObjectWithTag("BabyBody");
		heartMonitor = GameObject.Find("HeartMonitor").GetComponent<SWP_HeartRateMonitor> ();
		InitialState ();
		decompTimer = 600f;
		deathTimer = 900f;
		babyMaterial = babyBody.renderer.material;
		babyMaterial.SetFloat ("_Blend", 0.0f);
		mouthTarget = GameObject.Find ("mouthTarget").transform;

		EttVtPositioning ();
	}

	private void EttVtPositioning() {
		float x = 0.002780795f,
		y = -0.06677689f,
		z = -0.1790561f;
		ett.transform.parent = mouthTarget;
		ett.transform.localEulerAngles = new Vector3 (90, 0, 0);
		ett.transform.localPosition = new Vector3 (x, y, z);
		vt.transform.parent = ett.transform;
	}

	protected virtual void Start() {
		StartCoroutine(DecompState());
		StartCoroutine(DeathCondition());
		StartCoroutine(WinCondition());
	}

	protected virtual IEnumerator WinCondition() {
		while(!isCorrect) {
			yield return 0;
		}
		BabyRecovery();
		StopCoroutine("DeathCondition");
		StopCoroutine("DecompState");
	}

	protected virtual IEnumerator DecompState() {
		float time = 0f;
		while(time < decompTimer) {
			time += Time.deltaTime;
			yield return 0;
		} // Wait 300 seconds, if still in state 0--decomp the baby.
		if(currentState == 0) {
			FurtherDecomp();
		}
	}
	
	protected virtual IEnumerator DeathCondition() {
		yield return new WaitForSeconds(deathTimer);
		if(currentState != 2) { // If you haven't won after death time is up... kill the baby
			BabyDeath();
		}
	}

	// Update is called once per frame
	protected virtual void Update () {
		heartMonitor.BeatsPerMinute = bpm;
	}

	protected void UpdateMonitor() {
		MonitorUpdates.Instance.UpdateMonitor(Sp02,temperature,bloodPressure,heartRate);
	}
	
	protected void UpdateMonitor(float seconds) {
		MonitorUpdates.Instance.UpdateMonitor(Sp02,temperature,bloodPressure,heartRate,seconds);
	}
	
	// Initial state of baby
	protected virtual void InitialState() {
		Sp02 = "75%";
		temperature = "37.1";
		babyBreath.respRate = 90f;
		bloodPressure = "50/25";
		heartRate = "180";
		bpm = 180;

		// Right lung not working
		babyBreath.both = false;

		babyBody = GameObject.FindGameObjectWithTag("BabyBody");
		babyMaterial = babyBody.renderer.material;
		babyMaterial.SetFloat ("_Blend", 0.0f); // Healthy Lip

		UpdateMonitor();
	}
	
	// No needle decomp by 5 min (regardless of interations or lack thereof) or needle decomp in incorrect location
	protected virtual void FurtherDecomp() {
		Debug.Log ("Enter FurtherDecomp");
		currentState = 1;
		// Chest retraction
		// Nasal flaring
		// Grunting
		
		Sp02 = "60%";
		// Cyanosis enabled
		babyBreath.respRate = 120f;
		bloodPressure = "30/10";
		bpm = 220;
		heartRate = "220";

		babyMaterial.SetFloat ("_Blend", 1.0f); // Blue Lips

		UpdateMonitor();
	}
	
	// Needle decomp by 5 min in correct location
	protected virtual void BabyRecovery() {
		currentState = 2;
		
		// No retrations
		// No nasal flaring
		// No grunting
		
		Sp02 = "94%";
		babyBreath.respRate = 40f;
		bloodPressure = "65/35";
		bpm = 140;
		heartRate = "140";

		// Both lungs working
		babyBreath.both = true;

		Invoke ("ChangeScene", 60.0f);
		babyMaterial.SetFloat ("_Blend", 0.0f); // Healthy Lips

		UpdateMonitor(60f);
	}
	
	// No needle decomp by 10 min (5+5, regardless of interations or lack thereof) or needle decomp in incorrect location
	protected virtual void BabyDeath() {
		currentState = 3;
		
		// Lethargic
		// No chest retrations
		// No nasal flaring
		// No grunting
		
		// SpO2 30%
		Sp02 = "30%";
		// Cyanosis enabled
		// Respiratory rate 60 breathes/min
		babyBreath.respRate = 0f;
		// Blood pressure 15/5 mmHg
		bloodPressure = "15/5";
		// Heart rate 250 bpm
		bpm = 250;
		heartRate = "250";
		// Pusle strength absent
		
		// END SCENARIO WITH FAIL
		
		Invoke ("ChangeScene", 3.0f);
		babyMaterial.SetFloat ("_Blend", 1.0f); // Blue Lips
		UpdateMonitor();
	}

	// Overridable, cardiac case and any future cases may not always do the same thing or activate specific scenarios
	protected virtual void ChangeScene() {
		if (currentState == 2) {
			//CaseHandler.Instance.ActivateCardiac(); // Beating this activates the cardiac test.
			Application.LoadLevel ("Success");
		} else {
			CaseHandler.Instance.KillBaby();
			Application.LoadLevel ("Failure");
		}
	}
}
