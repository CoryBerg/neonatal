using UnityEngine;
using System.Collections;

public class AltText : MonoBehaviour {
	public NeonatalCase CaseToShow;
	public dfLabel labelToModify;
	public TextAsset text; // Can drag text from a file here
	public string value;
	// Use this for initialization
	void Start () {
		if(labelToModify == null) {
			labelToModify = this.GetComponent<dfLabel>();
		}
		if(CaseHandler.Instance.currentCase == CaseToShow) {
			if(text != null) {
				labelToModify.Text = text.text;
			} else {
				labelToModify.Text = value;
			}
		}
	}
}
