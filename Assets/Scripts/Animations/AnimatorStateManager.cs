using UnityEngine;
using System.Collections;

public class AnimatorStateManager {
	public GameObject armEtt, babyEtt, bagAndMask, ettJoint, iv, jointsGroup, laryn, needle1, needle2, vt, vtJoint;

	public Transform arms;

	public void CheckMecanimState (AnimatorStateInfo stateInfo) {
		if (stateInfo.IsName ("Enter Intubation")) {
			bagAndMask.SetActive (false);
			laryn.SetActive (false);
		} else if (stateInfo.IsName ("ETT -> Hand")) {
			babyEtt.SetActive (false);
			armEtt.SetActive (true);
		} else if (stateInfo.IsName ("Enter bagging")) {
			bagAndMask.SetActive (true);
		} else if (stateInfo.IsName ("Reintubation")) {
			bagAndMask.SetActive (false);
			laryn.SetActive (true);
		} else if (stateInfo.IsName ("ETT -> Baby")) {
			babyEtt.SetActive (true);
			armEtt.SetActive (false);
		} else if (stateInfo.IsName ("Exit Intubation")) {
			laryn.SetActive(false);
		} else if (stateInfo.IsName ("Needle Decomp")) {
			vtJoint.transform.parent = ettJoint.transform;

			arms.position = new Vector3(0.005775452f, 1.228995f, 5.056239f);
		} else if (stateInfo.IsName ("Swap Needle")) {
			needle1.SetActive (false);
			needle2.SetActive (true);
			needle2.transform.parent = null;
		} else if (stateInfo.IsName ("Reset Arms")) {
			vtJoint.transform.parent = jointsGroup.transform;
			//vtJoint.transform.localPosition = new Vector3(0.7489247f, 0.05975409f, 0.002217171f);
			ArmAnimatorController.Instance.ResetArms();
		} else if (stateInfo.IsName ("Enter Suction")) {
			iv.SetActive (false);
		} else if (stateInfo.IsName ("Suction")) {
			iv.SetActive (true);
			vt.SetActive (false);
		} else if (stateInfo.IsName("Exit Suction")) {
			iv.SetActive(false);
			vt.SetActive(true);
		}
	}
}
