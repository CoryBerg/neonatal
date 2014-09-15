using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmItemsContainer : MonoBehaviour {
	public static ArmItemsContainer Instance;
    public List<GameObject> butterFlyObs, suctionObs, intubationObjs, stethObs;
	private Dictionary<string, List<GameObject>> items;

	void Awake() {
		Instance = this;
		items = new Dictionary<string, List<GameObject>>();
		items.Add ("ButtonNeedle", butterFlyObs);
		items.Add ("ButtonSuction", suctionObs);
        items.Add("ButtonIntubation", intubationObjs);
		items.Add ("ButtonSteth", stethObs);
		DisableAllItems ();
	}

	public void NewAnimation(string animation) {
		DisableAllItems ();
		EnableItems (animation);
	}

	public void EnableItems(string key) {
		print ("Want to enable: " + key);// + items[key]);
		if(items.ContainsKey(key)) {
            foreach (GameObject i in items[key]) {
                i.SetActive(true);
            }
		}
	}

	public void DisableAllItems() {
		foreach (List<GameObject> ilist in items.Values) {
            foreach (GameObject i in ilist) {
			    i.SetActive (false);
            }
		}
	}
}
	