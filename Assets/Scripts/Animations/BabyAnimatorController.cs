using UnityEngine;
using System.Collections;

public class BabyAnimatorController : MonoBehaviour {
    public AnimationClip intubationClip;

	public ArmAnimatorController armCtrl;

	private Animator babyAnimator;
    public Animator AnimComponent {
        get { return babyAnimator; }
    }
	public static BabyAnimatorController Instance;

	// Use this for initialization
	void Awake() {
		Instance = this;
		babyAnimator = GetComponent<Animator> ();
        babyAnimator.speed = 0f;
	}

    public void SetRecovery() {
        StartCoroutine(Recovery());
    }

    IEnumerator Recovery() {
        float t = 0;
        while(t < 10f) {
            t += Time.deltaTime;
            SetSpeed(t / 10f);
            yield return null;
        }
    }

    void SetSpeed(float f) {
        babyAnimator.speed = f;
    }

    IEnumerator Intubate() {
		while(ArmAnimatorController.Instance.intubating == false) {
			yield return null;
		}
		float exitTime = intubationClip.length + .93f;
		float t = 0f;
		while(t < exitTime) {
			if(ArmAnimatorController.Instance.intubating == false) {
				SetSpeed(0f);
				yield return null;
				continue;
			} else {
				t += Time.deltaTime;
				SetSpeed(1f);
				yield return null;
				continue;
			}
		}
        //yield return new WaitForSeconds(intubationClip.length + .93f); // .9 is exit time of animation
        //SetSpeed(0f);
    }

	// Triggers mechanim state for animation
	public void TriggerAnimation(string animation) {
        SetSpeed(1f);
//		if (animation == "ButtonIntubation") {
//			babyAnimator.SetTrigger (animation);
//		}
//        //StartCoroutine(Intubate());
	}

	public void IntubateBabyBegin() {
		babyAnimator.SetTrigger ("ButtonIntubation");
	}

	void Update() {
//		print(animator.GetCurrentAnimatorStateInfo(0).
//		if(babyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Intubation")) {//.nameHash == Animator.StringToHash("Base.Intubation")) {
//			SetSpeed (1f);
//		}
	}
}
