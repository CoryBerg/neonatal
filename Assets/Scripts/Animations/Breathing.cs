using UnityEngine;
using System.Collections;

public class Breathing : MonoBehaviour {
	public float respRate = 50f;
	private float respiratory;
	public bool both = false;
	private float breath;
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
		breath = Mathf.Sin (Time.time * 2) * respRate/2 + 50;
		skinMeshRenderer.SetBlendShapeWeight (1, breath);
	}

	void BothLungs() {
		breath = Mathf.Sin(Time.time * 2) * respRate + 50;
		skinMeshRenderer.SetBlendShapeWeight(1, breath);
		skinMeshRenderer.SetBlendShapeWeight(2, breath);
	}
}
