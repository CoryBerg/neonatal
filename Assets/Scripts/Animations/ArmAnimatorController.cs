using UnityEngine;
using System.Collections;

public class ArmAnimatorController : MonoBehaviour {
	public static ArmAnimatorController Instance;
	public AudioClip armMove;
	public GameObject ett, vt, bagAndMask, mouthTarget, EttLeftHandTarget, needle1, needle2;
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
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Enter bagging")) {
			bagAndMask.SetActive(true);
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Remove ETT")) {
			bagAndMask.SetActive(false);
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("ETT -> Baby")) {
			px = 0.002780795f;
			py = -0.06677689f;
			pz = -0.1790561f;

			ett.transform.parent = mouthTarget.transform;
			ett.transform.localEulerAngles = new Vector3 (90, 0, 0);
			ett.transform.localPosition = Vector3.zero;
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Swap Needle")) {
			needle1.SetActive (false);
			needle2.SetActive (true);
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Exit Needle Decomp")) {
			needle1.SetActive (true);
			needle2.SetActive (false);
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
		animator.SetBool("InSteth", true);
		inSteth = true;
		transform.parent = target;
		transform.localPosition = Vector3.zero;
		ArmItemsContainer.Instance.NewAnimation ("ButtonSteth");
        animator.SetTrigger("ButtonSteth");
	}

    public void FinishSteth() {
		transform.parent = null;
        animator.SetBool("InSteth", false);
        inSteth = false;
        print("Steth Finsh");
    }

	IEnumerator CamDelay() {
		yield return new WaitForSeconds(1.5f);
		foreach (Camera camera in cameras) {
			camera.animation["chestCompZoom"].time = camera.animation["chestCompZoom"].length;
			camera.animation["chestCompZoom"].speed = -1.0f;
			camera.animation.Play("chestCompZoom");
		}
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
			StartCoroutine(CamDelay());
        }
		GameObject arms = GameObject.FindGameObjectWithTag ("Arms");
		AudioSource.PlayClipAtPoint (armMove, arms.transform.position);
		animator.SetTrigger (animName);
	}
}
