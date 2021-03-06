﻿using UnityEngine;
using System.Collections;

public class ArmAnimatorController : MonoBehaviour {
	public static ArmAnimatorController Instance;
	public AudioClip armMove;
	public GameObject armEtt, babyEtt, bagAndMask, ettJoint, iv, jointsGroup, laryn, needle1, vt, vtJoint;
	public Camera mainCamera;

	public bool intubating = false;

	private Animator animator;
	public Animator Anim {
		get {
			return animator;
		}
	}
	private AnimatorStateManager animatorStateManager;
	private ArmAnimationContainer animations;
	private Transform startingParent;
	private Vector3 startingLocalPos;
    private bool inSteth = false;
	// Use this for initialization
    void Awake() {
		animatorStateManager = new AnimatorStateManager () {
			arms = this.transform,
			armEtt = this.armEtt,
			babyEtt = this.babyEtt,
			bagAndMask = this.bagAndMask,
			ettJoint = this.ettJoint,
			iv = this.iv,
			jointsGroup = this.jointsGroup,
			laryn = this.laryn,
			needle1 = this.needle1,
			vt = this.vt,
			vtJoint = this.vtJoint
		};
        startingLocalPos = this.transform.localPosition;
		Instance = this;
		animations = new ArmAnimationContainer ();
		animator = GetComponent<Animator> ();
        startingParent = this.transform.parent;
	}

	// Called every frame
	void Update() {
		animatorStateManager.CheckMecanimState(animator.GetCurrentAnimatorStateInfo(0));
	}

	public void ResetArms() {
		this.transform.parent = startingParent;
		this.transform.localPosition = startingLocalPos;
	}

    IEnumerator MoveTo(Vector3 tar) {
        Vector3 start = transform.position;
        float t = 0f;
		float moveTime = .82f;
		while(t < moveTime) {
			float lVal = t / moveTime;
            transform.position = Vector3.Lerp(start,tar,lVal);
            t += Time.deltaTime;
            yield return null;
        }
    }

	IEnumerator PlaySound(string target) {
		yield return new WaitForSeconds (2.0f);
		Camera.main.audio.loop = true;

		if (target == "StethTargetUL") {
			Camera.main.audio.volume = 0.75f;
		} else {
			Camera.main.audio.volume = 0.15f;
		}

		Camera.main.audio.Play();
	}

	public void Stethescope(Transform target) {
		vtJoint.transform.parent = ettJoint.transform;

        if (inSteth) {
            StopCoroutine("MoveTo");
			StopCoroutine("PlaySound");
            StartCoroutine(MoveTo(target.position));
			StartCoroutine(PlaySound(target.name));
            print("Steth Transition");
            return;
        }

        print("Steth Begin");
		animator.SetBool("InSteth", true);
		inSteth = true;
		StartCoroutine(PlaySound(target.name));
		transform.parent = target;
		transform.localPosition = Vector3.zero;
		ArmItemsContainer.Instance.NewAnimation ("ButtonSteth");
        animator.SetTrigger("ButtonSteth");
	}

    public void FinishSteth() {
		vtJoint.transform.parent = jointsGroup.transform;
		vtJoint.transform.localPosition = new Vector3(0.7489247f, 0.05975409f, 0.002217171f);

		Camera.main.audio.loop = false;

		transform.parent = null;
        animator.SetBool("InSteth", false);
        inSteth = false;
		ResetArms ();
        print("Steth Finsh");
    }

	IEnumerator CamDelay() {
		yield return new WaitForSeconds(1.5f);
		mainCamera.animation["chestCompZoom"].time = mainCamera.animation["chestCompZoom"].length;
		mainCamera.animation["chestCompZoom"].speed = -1.0f;
		mainCamera.animation.Play("chestCompZoom");
	}

	// Triggers mechanim state for animation
	public void TriggerAnimation(string animation) {
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

	void intubationStart() {
		BabyAnimatorController.Instance.IntubateBabyBegin();
		print ("start intubate");
	}
	
	void intubationEnd() {
		print ("stop intubate");
	}
}
