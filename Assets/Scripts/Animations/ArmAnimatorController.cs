using UnityEngine;
using System.Collections;

public class ArmAnimatorController : MonoBehaviour {
	public static ArmAnimatorController Instance;
	public AudioClip armMove;
	public GameObject ett, vt, mouthTarget, EttLeftHandTarget;
	public Camera[] cameras;

	private Animator animator;
	private ArmAnimationContainer animations;
	private Transform startingParent;
	private Vector3 startingLocalPos;
	private bool doOnce = true;
    private bool inSteth = false;
	// Use this for initialization
    void Awake() {
        print("AWAKE START");
        startingLocalPos = this.transform.localPosition;
		Instance = this;
		animations = new ArmAnimationContainer ();
		animator = GetComponent<Animator> ();
        startingParent = this.transform.parent;

		//vt.transform.parent = ett.transform;
        print("AWAKE END");
	}

	// Called every frame
	void Update() {
		float px, py, pz,
			  rx, ry, rz;
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("ETT -> Hand")) {
			ett.transform.parent = EttLeftHandTarget.transform;
			ett.transform.localPosition = Vector3.zero;
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Insert Laryngoscope")) {
			ett.transform.parent = null;
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("ETT -> Baby")) {
			px = -0.1747731f;
			py = -0.2780856f;
			pz = -0.3187726f;
			rx = 39.43434f;
			ry = 0;
			rz = 328.235f;

			ett.transform.parent = EttLeftHandTarget.transform;
			ett.transform.localPosition = new Vector3(px, py, pz);
			ett.transform.localEulerAngles = new Vector3(rx, ry, rz);
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Finish Intubation")) {
			px = 0.002780795f;
			py = -0.06677689f;
			pz = -0.1790561f;

			ett.transform.parent = mouthTarget.transform;
			ett.transform.localEulerAngles = new Vector3 (90, 0, 0);
			ett.transform.localPosition = new Vector3 (px, py, pz);
		}
	}

	void ResetArms() {
		this.transform.parent = startingParent;
		this.transform.localPosition = startingLocalPos;
	}

    IEnumerator MoveTo(Vector3 tar) {
        Vector3 start = transform.position;
        float t = 0f;
        while(t < .82f) {
            float lVal = t / 2f;
            transform.position = Vector3.Lerp(start,tar,lVal);
            t += Time.deltaTime;
            yield return null;
        }
    }

	public void Stethescope(Transform target) {
        if (inSteth) {
            StopCoroutine("MoveTo");
            StartCoroutine(MoveTo(target.position));
            print("Steth Transition");
            return;
        }
        print("Steth Begin");
		transform.parent = target;
		transform.localPosition = Vector3.zero;
		ArmItemsContainer.Instance.NewAnimation ("ButtonSteth");
        animator.SetTrigger("ButtonSteth");
        animator.SetBool("InSteth", true);
        inSteth = true;
	}

    public void FinishSteth() {
        animator.SetBool("InSteth", false);
        inSteth = false;
        print("Steth Finsh");
    }

	// Triggers mechanim state for animation
	public void TriggerAnimation(string animation) {
		doOnce = true;
		ResetArms();
		ArmItemsContainer.Instance.NewAnimation (animation);
		
		string animName = animations.GetAnimation(animation);
		if(animName == "") {
			Debug.Log("Animation DNE");
			return;
		}
        if (animName == "EndCC") {
		foreach (Camera camera in cameras) {
			camera.animation["chestCompZoom"].time = camera.animation["chestCompZoom"].length;
			camera.animation["chestCompZoom"].speed = -1.0f;
			camera.animation.Play("chestCompZoom");
		}
        }
		GameObject arms = GameObject.FindGameObjectWithTag ("Arms");
		AudioSource.PlayClipAtPoint (armMove, arms.transform.position);
		animator.SetTrigger (animName);
	}
}
