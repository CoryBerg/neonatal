using UnityEngine;
using System.Collections;

public class Breathing : MonoBehaviour {
	public float respRate = 50f;
	public bool both = false;

	private float amp, breath, respiratory;

	private SkinnedMeshRenderer skinMeshRenderer;
	private float changeTime, changeTarget;
	// Use this for initialization
	void Start () {
		skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (both) {
			BothLungs ();
		} else {
			LeftLung ();
		}
	}

	void LeftLung() {
		amp = 65 - (3*respRate)/8;
		breath = 50 + amp * Mathf.Sin(respRate * Time.time / (10));
		skinMeshRenderer.SetBlendShapeWeight (1, breath);
	}

	void BothLungs() {
		amp = 65 - (3*respRate)/8;
		breath = 50 + amp * Mathf.Sin(respRate * Time.time / (10));
		skinMeshRenderer.SetBlendShapeWeight(1, breath);
		skinMeshRenderer.SetBlendShapeWeight(2, breath);
	}
}
