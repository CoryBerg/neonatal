using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmItemsContainer : MonoBehaviour {
	public static ArmItemsContainer Instance;
	public GameObject butterfly, maskAndBag, laryngoscope, steth;
	private Dictionary<string, GameObject> items;

	void Awake() {
		Instance = this;
		items = new Dictionary<string, GameObject>();
		items.Add ("ButtonNeedle", butterfly);
		items.Add ("ButtonSunction", maskAndBag);
		items.Add ("ButtonIntubation", laryngoscope);
		items.Add ("ButtonSteth", steth);
		DisableAllItems ();
	}

	public void NewAnimation(string animation) {
		DisableAllItems ();
		EnableItems (animation);
	}

	public void EnableItems(string key) {
		print ("Want to enable: " + key);// + items[key]);
		if(items.ContainsKey(key)) {
			items[key].SetActive(true);
		}
	}

	public void DisableAllItems() {
		foreach (GameObject item in items.Values) {
			item.SetActive (false);
		}
	}
}
	