using UnityEngine;
using System.Collections;

public class MakeToast : MonoBehaviour {
	public string toast;
	
	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
		print ("Toasting");
		TestHandler.Instance.Toast(toast);
	}
}
