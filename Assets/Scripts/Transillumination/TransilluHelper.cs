using UnityEngine;
using System.Collections;

public class TransilluHelper : MonoBehaviour {

	public GameObject transillu;


	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent) {
		Debug.Log (this.transform.name);
		
//		transillu.GetComponent<Transillumination>().isTriggered = true;
		
	}
}
