using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AltText : MonoBehaviour {

	public NeonatalCase CaseToShow;
	public Text textToModify;
	public string value;

	// Use this for initialization
	void Start () {
		if(CaseHandler.Instance.currentCase == CaseToShow) {
			if(value != null) {
				textToModify.GetComponent<Text> ().text = value;
			}
		}
	}
}
