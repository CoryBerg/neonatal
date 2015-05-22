using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverableText : MonoBehaviour {

	public string textToDisplay;

	private Tooltip tooltip; //initialize this by getting the script attached to the tooltip

	// Use this for initialization
	void Start () {
		tooltip = Tooltip.Instance;

		EventTrigger trig = this.gameObject.GetComponent<EventTrigger>();
		AddPointerEnterTrigger(trig, OnPointerEnter, EventTriggerType.PointerEnter);
		AddPointerExitTrigger(trig, OnPointerExit, EventTriggerType.PointerExit);
	}

	private void AddPointerEnterTrigger(EventTrigger evTrig, UnityAction<BaseEventData> action, EventTriggerType triggerType){
		Debug.Log ("hit 1");

		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		AddEventTrigger(evTrig, d => OnPointerEnter(d), EventTriggerType.PointerEnter);

		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };
		evTrig.delegates.Add(entry);
	}
	
	private void AddPointerExitTrigger(EventTrigger evTrig, UnityAction action, EventTriggerType triggerType){
		Debug.Log ("hit 2");
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		trigger.AddListener((eventData) => action());

		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };
		evTrig.delegates.Add(entry);
	}

	private void AddEventTrigger(EventTrigger evTrig, UnityAction<BaseEventData> action, EventTriggerType triggerType){
		Debug.Log ("hit 3");

		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		trigger.AddListener((eventData) => action(eventData));

		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };
		evTrig.delegates.Add(entry);
	}
	
	protected void OnPointerEnter(BaseEventData dataObject){ 
		Debug.Log ("pointerEnter");

		tooltip.SetTooltip(textToDisplay); 
	} 
	
	private void OnPointerExit(){ 
		tooltip.HideTooltip(); 
	}
}
