using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChestExamHider : MonoBehaviour {
	public List<dfButton> buttonsToHide; // inspector

	// Executed when button is pressed...
	public void Enable() {
		foreach(dfButton b in buttonsToHide) {
			b.IsVisible = true;
		}
	}

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent ) {
		Enable ();
	}

	// When it's selected from exam menu
	public void HideItems() {
		foreach(dfButton b in buttonsToHide) {
			b.IsVisible = false;
		}
	}
}
