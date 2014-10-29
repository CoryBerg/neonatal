using UnityEngine;
using System.Collections;

public class ColorModifier : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (CaseHandler.Instance.currentCase == NeonatalCase.Cardiac) {
			this.light.color = Color.white;
			this.light.intensity = 8.0f;
		}
	}

}
