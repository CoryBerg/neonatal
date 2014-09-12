using UnityEngine;
using System.Collections;

public class BabyAnimatorController : MonoBehaviour {
    public AnimationClip intubationClip;
	private Animator animator;
    public Animator AnimComponent {
        get { return animator; }
    }
	private BabyAnimationContainer animations;
	public static BabyAnimatorController Instance;

	// Use this for initialization
	void Awake() {
		Instance = this;
		animations = new BabyAnimationContainer ();
		animator = GetComponent<Animator> ();
        animator.speed = 0f;
	}

    public void SetRecovery() {
        StartCoroutine(Recovery());
    }

    IEnumerator Recovery() {
        float t = 0;
        while(t < 10f) {
            SetSpeed(t / 10f);
            yield return null;
        }
    }

    void SetSpeed(float f) {
        animator.speed = f;
    }

    IEnumerator Intubate() {
        yield return new WaitForSeconds(intubationClip.length + .93f); // .9 is exit time of animation
        SetSpeed(0f);
    }

	// Triggers mechanim state for animation
	public void TriggerAnimation(string animation) {
        SetSpeed(1f);
		if (animation == "ButtonIntubation") {
			animator.SetTrigger (animation);
		}
        StartCoroutine(Intubate());
		//float t = animations.GetAnimation(animation);
		//if(t == BabyAnimationContainer.ANIM_SENTINEL) {
		//	print ("Baby animation not found.");
		//	return;
		//}
		//animator.SetFloat ("State", t);
	}
}
