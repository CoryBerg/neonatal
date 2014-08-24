using UnityEngine;
using System.Collections;

public class ArmAnimatorController : MonoBehaviour {
	public static ArmAnimatorController Instance;
	public AudioClip armMove;

	private Animator animator;
	private ArmAnimationContainer animations;
	private Transform startingParent;
	private Vector3 startingLocalPos;
	private bool doOnce = true;

	// Use this for initialization
    void Awake() {
        startingLocalPos = this.transform.localPosition;
		Instance = this;
		animations = new ArmAnimationContainer ();
		animator = GetComponent<Animator> ();
		startingParent = this.transform.parent;
	}

	// Called every frame
	void Update() {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle") && doOnce) {
			doOnce = false;
			GameObject.FindGameObjectWithTag("Baby").GetComponent<Animator>().SetFloat("Intubation", 0.0f);
		}
	}

	void ResetArms() {
		this.transform.parent = startingParent;
		this.transform.localPosition = startingLocalPos;
	}

	public void Stethescope(Transform target) {
		transform.parent = target;
		transform.localPosition = Vector3.zero;
		ArmItemsContainer.Instance.NewAnimation ("ButtonSteth");
		animator.SetTrigger("ButtonSteth");
	}

	// Triggers mechanim state for animation
	public void TriggerAnimation(string animation) {
		ResetArms();
		ArmItemsContainer.Instance.NewAnimation (animation);

		if(animation == "ButtonIntubation") {
			GameObject.FindGameObjectWithTag("Baby").GetComponent<Animator>().SetFloat("Intubation", 0.5f);
			doOnce = true;
		}
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
