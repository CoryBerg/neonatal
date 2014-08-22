using UnityEngine;
using System.Collections;

public class ShowLabPanel : MonoBehaviour {
	public dfPanel labPanel;
	public dfPanel testPanel;

	// The df event bindings weren't working for this button so I made this work around.
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent ) {
		labPanel.IsVisible = true;
		testPanel.IsVisible = false;
	}
}
