using UnityEngine;
using System.Collections;

public class babyStateDisplay : MonoBehaviour {
	public RespiratoryCase respiratoryCase;
	int state;
	public dfLabel lbl;

	void Start() {
		StartCoroutine(Fade ());
	}

	IEnumerator Fade() {
		float delay = 4f;
		yield return new WaitForSeconds(delay);
		float t = 0;
		while(t < 2f) {
			t += Time.deltaTime;
			lbl.Opacity = 1 - t / 2f;
			yield return null;
		}
	}
	
	// diaplay the state of baby with gui lable
	void Update() {
		state = CaseInitializer.Instance.ActiveCase.currentState;	
		switch(state) {	
		case 0:
			GetComponent<dfLabel>().Text = "This baby isn't looking so good...";
			break;
			
		case 1:
			GetComponent<dfLabel>().Text = "Respiratory rate has increased. Blood pressure at 30/10.";
			break;
			
		case 2:
			GetComponent<dfLabel>().Text = "Baby has recovered!";
			break;
			
		case 3:
			GetComponent<dfLabel>().Text = "I'm not detecting a pulse!";
			break;
		}
	}
}
