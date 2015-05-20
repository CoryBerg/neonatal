using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour {

	public bool followPointer;
	public GameObject tooltip;
	public string tooltipText;

	// Use this for initialization
	void Start() {
		followPointer = false;
	}
	
	// Update is called once per frame
	void Update() {
		if(followPointer) {
			tooltip.transform.position = Input.mousePosition;
		}
	}

	public void ShowTooltip() {
		tooltip.SetActive(true);
		followPointer = true;
	}

	public void HideTooltip() {
		tooltip.SetActive(false);
		followPointer = false;
	}

	public void OnPointerEnter(PointerEventData data) {
		ShowTooltip();
	}

	public void OnPointerExit(PointerEventData data) {
		HideTooltip();
	}
}
