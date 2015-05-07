using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(Application.isWebPlayer) {
			Destroy(this.gameObject);
		}
	}
	
	public void ExitGame() {
		Application.Quit();
	}
}
