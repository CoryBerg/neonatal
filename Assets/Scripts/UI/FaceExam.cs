using UnityEngine;
using System.Collections;

public class FaceExam : MonoBehaviour {

	public Camera mainCamera;

	public void EnterFaceExam() {
		mainCamera.animation ["faceExam"].speed = 1f;
		mainCamera.animation["faceExam"].time = 0;
		mainCamera.animation.Play("faceExam");
	}

	public void ExitFaceExam() {
		mainCamera.animation ["faceExam"].speed = -1f;
		mainCamera.animation["faceExam"].time = mainCamera.animation["faceExam"].length;
		mainCamera.animation.Play("faceExam");
	}
}
