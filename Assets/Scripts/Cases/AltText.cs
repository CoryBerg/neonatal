using UnityEngine;
using System.Collections;

public class AltText : MonoBehaviour {
	public NeonatalCase CaseToShow;
	public dfLabel labelToModify;
	public dfButton buttonToModify;
	public TextAsset text; // Can drag text from a file here
	public string value;
	// Use this for initialization
	void Start () {
		if(buttonToModify != null) {
			if(CaseHandler.Instance.currentCase == CaseToShow) {
				if(text != null) {
					buttonToModify.Text = text.text;
				} else {
					buttonToModify.Text = value;
				}
				return;
			}
		} else if(labelToModify == null) {
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
