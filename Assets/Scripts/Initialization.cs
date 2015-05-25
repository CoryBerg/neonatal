using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Initialization : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteKey("buttonCount");
		Caching.CleanCache ();
	}
}
