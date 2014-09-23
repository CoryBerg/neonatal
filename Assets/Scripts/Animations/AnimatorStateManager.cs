using UnityEngine;
using System.Collections;

public class AnimatorStateManager {
	public GameObject babyEtt, armEtt, vt, bagAndMask, leftHand, mouthTarget, EttLeftHandTarget, needle1, needle2;

	public void CheckMecanimState (AnimatorStateInfo stateInfo) {
		if (stateInfo.IsName ("ETT -> Hand")) {
			babyEtt.SetActive (false);
			armEtt.SetActive (true);
		} else if (stateInfo.IsName ("Enter bagging")) {
			bagAndMask.SetActive (true);
		} else if (stateInfo.IsName ("Remove ETT")) {
			bagAndMask.SetActive (false);
		} else if (stateInfo.IsName ("ETT -> Baby")) {
			babyEtt.SetActive (true);
			armEtt.SetActive (false);
		} else if (stateInfo.IsName ("Swap Needle")) {
			needle1.SetActive (false);
			needle2.SetActive (true);
		} else if (stateInfo.IsName ("Exit Needle Decomp")) {
			needle1.SetActive (true);
			needle2.SetActive (false);
		} else if (stateInfo.IsName ("Suction")) {
			vt.SetActive (false);
		} else if (stateInfo.IsName("Exit Suction")) {
			vt.SetActive(true);
		}
	}
}
