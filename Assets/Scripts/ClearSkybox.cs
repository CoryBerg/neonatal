using UnityEngine;
using System.Collections;

public class StupidScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Camera>().clearFlags = CameraClearFlags.Depth; // Why was this skybox before?  It's a gui camera.
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
