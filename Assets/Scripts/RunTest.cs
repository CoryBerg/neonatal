using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunTest : MonoBehaviour {
	public GameObject testResultPanel;

	public Text textToColor;

	public string testName;
	public float testLength;

	public bool isLabTest;


	public void ClickTest () {
		if(TestHandler.Instance.TestStatus(testName) && TestHandler.Instance.TestsCompleted[testName]) {
			if(isLabTest) {
				InGameMenuUI.Instance.OpenLabTestPanel(testResultPanel);
			} else {
				InGameMenuUI.Instance.OpenTestPanel(testResultPanel);
			}
		} else if(!TestHandler.Instance.TestStatus(testName)) {
			TestHandler.Instance.BeginTest(testName, testLength);
			SetButtonColor(Color.red);
			Invoke("SetComplete", testLength);
		} else {
			TestHandler.Instance.StartCoroutine("ResultsSoon");
		}
	}

	void SetComplete() {
		SetButtonColor(Color.green);
	}
	
	void SetButtonColor(Color c) {
		textToColor.color = c;
	}
}
