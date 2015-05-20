using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestHandler : MonoBehaviour {
	// Use this for initialization
	public Dictionary<string,bool> TestsCompleted; // if dict has key, it's in progress, if not--not started.
	public static TestHandler Instance;

	public Image notificationFrameObject;
	public Text notificationTextObject;
	//public dfTweenFloat tweener, reverseTweener;// If true... results are available

	void Start () {
		TestsCompleted = new Dictionary<string, bool>();
		Instance = this;

		notificationFrameObject.canvasRenderer.SetAlpha(0.0f);
		notificationTextObject.canvasRenderer.SetAlpha(0.0f);
	}

	public void BeginTest(string testName, float testLength) {
		StartCoroutine("DoTest", new TestStuff(testName, testLength));
	}

	public IEnumerator ResultsSoon() {
		notificationTextObject.text = "Results Available Soon";
		
		FadeIn();

		yield return new WaitForSeconds(4.5f);
		
		FadeOut();
	}

    public void Toast(string toast) {
        StartCoroutine(GenericToast(toast));
    }

    public IEnumerator GenericToast(string toast) {
		notificationTextObject.text = toast;
        // This tweening will be buggy.

		FadeIn();

        yield return new WaitForSeconds(4.5f);

		FadeOut();
    }

	IEnumerator DoTest(TestStuff aTest) {
		TestsCompleted[aTest.name] = false;
		
		notificationTextObject.text = aTest.name + " Test Begun";

		FadeIn();

		float t = 0;
		while(t <= 2.5f) {
			t += Time.deltaTime;
			yield return 0;
		}

		FadeOut();

		yield return new WaitForSeconds(aTest.length - t);

		TestsCompleted[aTest.name] = true;

		notificationTextObject.text = aTest.name + " Results Available. Please click on test to view results.";
		
		FadeIn();
		
		yield return new WaitForSeconds(4.5f);

		FadeOut();
	}

	public bool TestStatus(string testName) {
		// If the test does not exist in the dictionary OR
		if(!TestsCompleted.ContainsKey(testName)) {
			return false;
		}

		return true;
	}

	private void FadeIn() {
		notificationFrameObject.canvasRenderer.SetAlpha(0.0f);
		notificationTextObject.canvasRenderer.SetAlpha(0.0f);
		notificationFrameObject.CrossFadeAlpha(255.0f, 100.0f, true);
		notificationTextObject.CrossFadeAlpha(255.0f, 100.0f, true);
	}

	private void FadeOut() {
		notificationFrameObject.CrossFadeAlpha(-255.0f, 100.0f, true);
		notificationTextObject.CrossFadeAlpha(-255.0f, 100.0f, true);
	}
}

public class TestStuff {
	public string name;
	public float length;

	public TestStuff(string n, float l) {
		this.name = n;
		this.length = l;
	}
}
