using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaseInitializer : MonoBehaviour {
	public static CaseInitializer Instance;
	public RespiratoryCase ActiveCase;

	[Header ("Answer Buttons")]
	public GameObject respAnswerButton;
	public GameObject cardiacAnswerButton;

	// Use this for initialization
	void Awake () {
		if(Instance != null) {
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
		if(CaseHandler.Instance.currentCase == NeonatalCase.Respiratory) {
			ActiveCase = this.gameObject.AddComponent<RespiratoryCase>();
			respAnswerButton.GetComponent<Button>().onClick.AddListener(() => this.gameObject.GetComponent<RespiratoryCase>().CorrectChoiceMade());
		} else if(CaseHandler.Instance.currentCase == NeonatalCase.Cardiac) {
			ActiveCase = this.gameObject.AddComponent<CardiacCase>();
			cardiacAnswerButton.GetComponent<Button>().onClick.AddListener(() => this.gameObject.GetComponent<CardiacCase>().CorrectChoiceMade());
        } else if (CaseHandler.Instance.currentCase == NeonatalCase.Tutorial) {
            ActiveCase = this.gameObject.AddComponent<TutorialCase>();
        }
	}
}
