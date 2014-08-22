using UnityEngine;
using System.Collections;

public class CaseInitializer : MonoBehaviour {
	public static CaseInitializer Instance;
	public RespiratoryCase ActiveCase;
	public MakeChoice respAnswer, cardiacAnswer;
	// Use this for initialization
	void Awake () {
		if(Instance != null) {
			Destroy(this.gameObject);
			return;
		}
		Instance = this;
		if(CaseHandler.Instance.currentCase == NeonatalCase.Respiratory) {
			ActiveCase = this.gameObject.AddComponent<RespiratoryCase>();
			respAnswer.IsAnswer = true;
		} else if(CaseHandler.Instance.currentCase == NeonatalCase.Cardiac) {
			ActiveCase = this.gameObject.AddComponent<CardiacCase>();
			cardiacAnswer.IsAnswer = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
