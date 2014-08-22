using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class CustomTextPopUp : MonoBehaviour {

	public bool showCurrentOperation = true;

	private GameObject popup;
	public string text;
//	private int clickCount = 0;


	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
		Debug.Log (this.transform.name);

		if (showCurrentOperation) 
		{

			StartCoroutine (ShowMessage (text, 3));
		}


	}
	

	IEnumerator ShowMessage (string message, float delay) {

		popup = GameObject.Find ("PopupMessage");

		popup.GetComponent<dfLabel>().Text = message;
		popup.GetComponent<dfLabel>().IsVisible = true;
		yield return new WaitForSeconds(delay);
		popup.GetComponent<dfLabel>().IsVisible = false;
	}
}