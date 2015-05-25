using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaseInitializer : Singleton<CaseInitializer> {
	public Case ActiveCase;

	[Header ("Answer Buttons")]
	public GameObject respAnswerButton;
	public GameObject cardiacAnswerButton;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();

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
