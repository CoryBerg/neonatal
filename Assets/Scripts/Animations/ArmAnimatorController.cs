﻿using UnityEngine;
using System.Collections;

public class ArmAnimatorController : MonoBehaviour {
	public static ArmAnimatorController Instance;
	public AudioClip armMove;
	public GameObject armEtt, babyEtt, bagAndMask, iv, laryn, leftHand, mouthTarget, needle1, needle2, vt;
	public Camera mainCamera;

	private Animator animator;
	private AnimatorStateManager animatorStateManager;
	private ArmAnimationContainer animations;
	private Transform startingParent;
	private Vector3 startingLocalPos;
    private bool inSteth = false;
	// Use this for initialization
    void Awake() {
        print("AWAKE START");
		animatorStateManager = new AnimatorStateManager () {
			armEtt = this.armEtt,
			babyEtt = this.babyEtt,
			bagAndMask = this.bagAndMask,
			iv = this.iv,
			laryn = this.laryn,
			leftHand = this.leftHand,
			mouthTarget = this.mouthTarget,
			needle1 = this.needle1,
			needle2 = this.needle2,
			vt = this.vt
		};
        startingLocalPos = this.transform.localPosition;
		Instance = this;
		animations = new ArmAnimationContainer ();
		animator = GetComponent<Animator> ();
        startingParent = this.transform.parent;

        print("AWAKE END");
	}

	// Called every frame
	void Update() {
		animatorStateManager.CheckMecanimState(animator.GetCurrentAnimatorStateInfo(0));
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
}
