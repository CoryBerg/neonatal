using UnityEngine;
using System.Collections;

public abstract class Case : MonoBehaviour {

	public Breathing babyBreath;
	public bool isCorrect = false;
	public float timer = 0.0f;
	public int bpm;
	public int currentState = 0;
	public string heartRate, Sp02, bloodPressure, temperature;
	
	protected GameObject babyBody;
	protected Material babyMaterial;
	protected float decompTimer, deathTimer;
	protected SWP_HeartRateMonitor heartMonitor;
	protected GameObject babyEtt;
	
	public void CorrectChoiceMade() {
		isCorrect = true;
	}
	
	protected virtual void Awake() {
		babyBreath = BabyAnimatorController.Instance.gameObject.GetComponent<Breathing> ();
		babyBody = GameObject.FindGameObjectWithTag("BabyBody");
		babyMaterial = babyBody.renderer.material;
		heartMonitor = GameObject.Find("HeartMonitor").GetComponent<SWP_HeartRateMonitor> ();
		
		InitialState ();

		babyEtt = ArmAnimatorController.Instance.babyEtt;
		
		EttPositioning ();
	}
	
	protected IEnumerator WarningCoroutine() {
		yield return new WaitForSeconds(deathTimer - 60f * 5f); // 5 minutes before failure...
		TestHandler.Instance.Toast("The baby is looking worse.");
	}
	
	protected void EttPositioning() {
		babyEtt.SetActive (true);
	}
	
	protected virtual void Start() {
		StartCoroutine(DecompState());
		StartCoroutine(DeathCondition());
		StartCoroutine(WinCondition());
		StartCoroutine(WarningCoroutine());
		StartCoroutine(DBGAdvance());
	}
	
	protected IEnumerator DBGAdvance() {
		while (true) {
			float t = 0;
			bool con = Input.GetKeyDown(KeyCode.A);
			while (con) {
				con = Input.GetKey(KeyCode.A);
				t += Time.deltaTime;

				if (t > 5f) {
					if (currentState == 0) {
						decompTimer = 0;
						print("Force further decomp");
					} else if (currentState == 1) {
						deathTimer = 0;
						print("Force death");
					}

					t = 0f;
					break;
				}

				yield return null;
			}

			yield return null;
		}
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
		}
		if(currentState == 0) {
			FurtherDecomp();
		}
	}
	
	protected virtual IEnumerator DeathCondition() {
		float time = 0f;
		while (time < deathTimer) {
			time += Time.deltaTime;
			yield return 0;
		}

		if(currentState != 2) {
			BabyDeath();
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		heartMonitor.BeatsPerMinute = bpm;
	}
	
	protected void UpdateMonitor() {
		UpdateMonitor(15f);
	}
	
	protected void UpdateMonitor(float seconds) {
		MonitorUpdates.Instance.UpdateMonitor(Sp02,babyBreath.respRate.ToString(),bloodPressure,heartRate,seconds);
	}
	
	protected IEnumerator updateNeedleDecompAfterDelay() { // values from paul
		yield return new WaitForSeconds(7f);
		TestHandler.Instance.Toast("Baby shows sign of Recovery");
		UpdateMonitor(15f);
	}
	
	// Initial state of baby
	protected virtual void InitialState() {
		Sp02 = "75%";
		temperature = "37.1";
		babyBreath.respRate = 90f;
		bloodPressure = "40/25";
		heartRate = "180";
		bpm = 180;
	}
	
	protected virtual void FurtherDecomp() {
		Debug.Log ("Enter FurtherDecomp");
		currentState = 1;
	}
	
	protected IEnumerator LipsOn(float t) {
		float start = t;
		while (t > 0) {
			t -= Time.deltaTime;
			SetBlend(1f - t / start);
			yield return null;
		}
	}
	
	protected void SetBlend(float t) {
		babyMaterial.SetFloat("_Blend", t);
	}
	
	
	protected IEnumerator LipsOff(float t) {
		float start = t;
		while (t > 0) {
			t -= Time.deltaTime/4;
			SetBlend(t / start);
			yield return null;
		}
	}
	
	// Needle decomp by 5 min in correct location
	protected virtual void BabyRecovery() {
		currentState = 2;
		
		Invoke("ChangeScene", 37.0f);
		
		BabyAnimatorController.Instance.SetRecovery();
	}
	
	protected virtual void BabyDeath() {
		currentState = 3;

		// END SCENARIO WITH FAIL
		Invoke("ChangeScene", 15.0f);
	}
	
	protected virtual void ChangeScene() {
		if (currentState == 2) {
			Application.LoadLevel ("Success");
		} else {
			CaseHandler.Instance.KillBaby();
			Application.LoadLevel ("Failure");
		}
	}
}
