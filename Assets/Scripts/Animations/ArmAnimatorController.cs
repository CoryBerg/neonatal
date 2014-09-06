using UnityEngine;
using System.Collections;

public class ArmAnimatorController : MonoBehaviour {
	public static ArmAnimatorController Instance;
	public AudioClip armMove;
	public GameObject ett, vt;

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
        print("AWAKE END");
	}

	// Called every frame
	void Update() {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Remove ETT")) {
			float x = 357.3963f;
			float y = 223.2345f;
			float z = 147.7629f;
			ett.transform.parent = GameObject.Find("L_Hand").transform;
			ett.transform.position = new Vector3(x,y,z);
			vt.transform.parent = GameObject.Find("mesh_grp").transform;
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

		GameObject arms = GameObject.FindGameObjectWithTag ("Arms");
		AudioSource.PlayClipAtPoint (armMove, arms.transform.position);
		animator.SetTrigger (animName);
	}
}
