using UnityEngine;
using System.Collections;

public class DoTest : MonoBehaviour {
	public string TestName;
	public float TestLength;
	public dfPanel resultPanel;
	public dfPanel testPanel;
	public dfButton testButton;

	void Awake() {
		testButton = this.GetComponent<dfButton>();
	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
		if(TestHandler.Instance.TestStatus(TestName)) {
			resultPanel.IsVisible = true;
			testPanel.IsVisible = false;
		} else if(!TestHandler.Instance.TestStatus(TestName)) {
			TestHandler.Instance.BeginTest(TestName,TestLength);
			SetButtonColor(Color.red);
			Invoke("SetComplete",TestLength);
		} else {
			TestHandler.Instance.StartCoroutine("ResultsSoon");
			// Should do something if it's already in progress
		}
	}

	void SetComplete() {
		SetButtonColor(Color.green);
	}

	void SetButtonColor(Color c) {
		testButton.TextColor = c;
		testButton.PressedTextColor = c;
		testButton.HoverTextColor = c;
		testButton.FocusTextColor = c;
		testButton.DisabledTextColor = c;
	}
}
