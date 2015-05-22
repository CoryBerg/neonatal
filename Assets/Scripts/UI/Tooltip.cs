using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tooltip : Singleton<Tooltip> {

	public Text text;

	public float xShift = 300.0f;
	public float yShift;

	public int canvasMode;

	private bool inside;

	private Canvas canvas;

	void Start() {
		this.gameObject.SetActive(false);

		canvas = GameObject.Find("Evaluation Menu").GetComponent<Canvas>();;
	}

	public void SetTooltip(string ttext) {
		text.text = ttext;
		
		//this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(text.preferredWidth + 500f, text.preferredHeight + 300f);

		xShift = -((this.transform.GetComponent<RectTransform>().rect.width * canvas.scaleFactor) / 2) - 15;
		yShift = (this.transform.GetComponent<RectTransform>().rect.height/ 2 * canvas.scaleFactor);

		Vector3 newPos = Input.mousePosition - new Vector3(xShift, yShift, 0f);

		this.transform.position = newPos;
		this.gameObject.SetActive(true);
		
		inside = true;
	}
	
	public void HideTooltip() {
		xShift = 40f;
		yShift = -30f;

		this.transform.position = Input.mousePosition - new Vector3(xShift, yShift, 0f);
		this.gameObject.SetActive(false);

		inside = false;
	}
	
	void FixedUpdate() {
		if(inside) {
			xShift = -((this.transform.GetComponent<RectTransform>().rect.width * canvas.scaleFactor) / 2) - 15;
			yShift = (this.transform.GetComponent<RectTransform>().rect.height/ 2 * canvas.scaleFactor);
			Vector3 newPos = Input.mousePosition - new Vector3(xShift, yShift, 0f);

			this.transform.position = newPos;
		}
	}
}
