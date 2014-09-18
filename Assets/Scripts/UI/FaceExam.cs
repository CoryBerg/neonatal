using UnityEngine;
using System.Collections;

public class FaceExam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void On() {
		Camera.main.animation ["faceExam"].speed = 1f;
		Camera.main.animation["faceExam"].time = 0;
		Camera.main.animation.Play("faceExam");
	}

	public void Off() {
		Camera.main.animation ["faceExam"].speed = -1f;
		Camera.main.animation["faceExam"].time = Camera.main.animation["faceExam"].length;
		Camera.main.animation.Play("faceExam");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
