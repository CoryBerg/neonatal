using UnityEngine;
using System.Collections;

// Also acting as parent class for all cases
public class RespiratoryCase : MonoBehaviour {
	public Breathing babyBreath;
	public bool isCorrect = false;
	public float timer = 0.0f;
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

	private GameObject babyEtt;

	protected virtual void Awake() {
		babyBreath = BabyAnimatorController.Instance.gameObject.GetComponent<Breathing> ();
        if (babyBreath == null) {
            print("WHY NULL.");
        }

		babyBody = GameObject.FindGameObjectWithTag("BabyBody");
		babyMaterial = babyBody.renderer.material;

		heartMonitor = GameObject.Find("HeartMonitor").GetComponent<SWP_HeartRateMonitor> ();

		InitialState ();
		decompTimer = 600f;
		deathTimer = 900f;

        StartCoroutine(LipsOn(3f));

		if (ArmAnimatorController.Instance == null) {
			print("AHH");
		}
		babyEtt = ArmAnimatorController.Instance.babyEtt;

		EttPositioning ();
	}

    private IEnumerator WarningCoroutine() {
        yield return new WaitForSeconds(deathTimer - 60f * 5f); // 5 minutes before failure...
        TestHandler.Instance.Toast("The baby is looking worse.");
    }

	private void EttPositioning() {
		babyEtt.SetActive (true);
	}

	protected virtual void Start() {
		StartCoroutine(DecompState());
		StartCoroutine(DeathCondition());
		StartCoroutine(WinCondition());
        StartCoroutine(WarningCoroutine());
        StartCoroutine(DBGAdvance());
	}

    IEnumerator DBGAdvance() {
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
		if(currentState != 2) { // If you haven't won after death time is up... kill the baby
			BabyDeath();
		}
	}

	// Update is called once per frame
	protected virtual void Update () {
		heartMonitor.BeatsPerMinute = bpm;
	}

	protected void UpdateMonitor() {
        UpdateMonitor(30f);
		//MonitorUpdates.Instance.UpdateMonitor(Sp02,temperature,bloodPressure,heartRate);
	}
	
	protected void UpdateMonitor(float seconds) {
		MonitorUpdates.Instance.UpdateMonitor(Sp02,temperature,bloodPressure,heartRate,seconds);
	}

    IEnumerator updateNeedleDecompAfterDelay() { // values from paul
        yield return new WaitForSeconds(7f);
        UpdateMonitor(30f);
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

        StartCoroutine(LipsOn(3f));
		
		UpdateMonitor();
	}

    protected IEnumerator LipsOn(float t) {
        float start = t;
        while (t > 0) {
            t -= Time.deltaTime;
            SetBlend(1f - t / start);
            yield return null;
        }
    }

    void SetBlend(float t) {
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

        Invoke("ChangeScene", 37.0f);

        StartCoroutine(LipsOff(30f));
        BabyAnimatorController.Instance.SetRecovery();
        StartCoroutine(updateNeedleDecompAfterDelay());
		//UpdateMonitor(15f);
	}

	// No needle decomp by 10 min (5+5, regardless of interations or lack thereof) or needle decomp in incorrect location
	protected virtual void BabyDeath() {
		currentState = 3;
		
		Sp02 = "30%";
		// Cyanosis enabled
		babyBreath.respRate = 0f;
		bloodPressure = "15/5";
		bpm = 0;
		heartRate = "0";

		// END SCENARIO WITH FAIL
        Invoke("ChangeScene", 15.0f);

        StartCoroutine(LipsOn(3f));
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
