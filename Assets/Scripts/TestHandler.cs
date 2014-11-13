using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestHandler : MonoBehaviour {
	// Use this for initialization
	public dfLabel lbl;
	public Dictionary<string,bool> TestsCompleted; // if dict has key, it's in progress, if not--not started.
	public static TestHandler Instance;
	public dfTweenFloat tweener, reverseTweener;// If true... results are available

	public class TestStuff {
		public string name;
		public float length;
		public TestStuff (string n, float l) {
			this.name = n;
			this.length = l;
		}
	}


	void Start () {
		TestsCompleted = new Dictionary<string, bool>();
		Instance = this;
	}

	public void BeginTest(string testName, float testLength) {
		StartCoroutine("DoTest",new TestStuff(testName,testLength));
	}

	public IEnumerator ResultsSoon() {
		
		lbl.Text = "Results Available Soon";
		// This tweening will be buggy.
		if(!tweener.IsPlaying) {
			tweener.Play();
		}
		yield return new WaitForSeconds(4.5f);
		if(!reverseTweener.IsPlaying) {
			reverseTweener.Play();
		}
	}

    public void Toast(string toast) {
        StartCoroutine(GenericToast(toast));
    }

    public IEnumerator GenericToast(string toast) {

        lbl.Text = toast;
        // This tweening will be buggy.
        if (!tweener.IsPlaying) {
            tweener.Play();
        }
        yield return new WaitForSeconds(4.5f);
        if (!reverseTweener.IsPlaying) {
            reverseTweener.Play();
        }
    }

	IEnumerator DoTest(TestStuff aTest) {
		TestsCompleted[aTest.name] = false;
		
		lbl.Text = aTest.name + " Test Begun";
		// This tweening will be buggy.
		if(!tweener.IsPlaying) {
			tweener.Play();
		}
		float t = 0;
		while(t <= 2.5f) {
			t += Time.deltaTime;
			yield return 0;
		}
		if(!reverseTweener.IsPlaying) {
			reverseTweener.Play();
		}
		yield return new WaitForSeconds(aTest.length - t);
		TestsCompleted[aTest.name] = true;
		lbl.Text = aTest.name + " Results Available. Please click on test to view results.";
		// This tweening will be buggy.
		if(!tweener.IsPlaying) {
			tweener.Play();
		}
		yield return new WaitForSeconds(4.5f);
		if(!reverseTweener.IsPlaying) {
			reverseTweener.Play();
		}
	}



	public bool TestStatus(string testName) {
		if(!TestsCompleted.ContainsKey(testName)) {
			return false;
		}
		return TestsCompleted[testName];
	}

	public bool TestInProgressButNotComplete(string testName) {
		return TestsCompleted.ContainsKey(testName) && !TestsCompleted[testName];
	}
}
