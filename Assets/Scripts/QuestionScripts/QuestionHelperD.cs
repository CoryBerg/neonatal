using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionHelperD : MonoBehaviour {
	
	public GameObject qReader;
	
	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
		Debug.Log (this.transform.name);

		qReader.GetComponent<QuestionReader>().dPressed = true;
		
	}

}
