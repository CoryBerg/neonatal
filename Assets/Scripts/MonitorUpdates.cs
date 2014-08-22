using UnityEngine;
using System.Collections;

public class MonitorUpdates : MonoBehaviour {
	public dfLabel pressure, hRate, spO2, temp;
	public static MonitorUpdates Instance;
	public float updateGranularity = .1f; // How quickly the monitor updates. Needs to be longer than the length of a frame\, suggested minimum is .1

	public class LabelTween {
		public float length;
		public dfLabel label;
		public float target;
		public LabelTween(float l, dfLabel lbl, float t) {
			length = l;
			label = lbl;
			target = t;
		}
	}
	void Awake() {
		Instance = this;
	}

	public void UpdateMonitor(string so2, string t, string bp, string hr) {
		spO2.Text = so2;
		temp.Text = t;
		pressure.Text = bp;
		StopCoroutine("MonitorTween");
		StartCoroutine("MonitorTween",new LabelTween(2.5f,hRate,float.Parse(hr)));
	}
	
	public void UpdateMonitor(string so2, string t, string bp, string hr, float seconds) {
		spO2.Text = so2;
		temp.Text = t;
		pressure.Text = bp;
		StopCoroutine("MonitorTween");
		StartCoroutine("MonitorTween",new LabelTween(seconds,hRate,float.Parse(hr)));
	}

	IEnumerator MonitorTween(LabelTween lt) {
		float t = 0;
		float start = float.Parse(lt.label.Text);
		while(t < lt.length) {
			t += updateGranularity;
			if(t > lt.length)
				t = lt.length;
			float lerpVal = Mathf.Lerp(start, lt.target, t/lt.length);
			lt.label.Text = ((int)lerpVal).ToString();
			yield return new WaitForSeconds(updateGranularity);
		}
		while(true) { // Fluctuates the heart rate a bit
			float c = float.Parse(lt.label.Text);
			float tar = lt.target * Random.Range(.95f,1.08f);
			float lerpSpeed = Random.Range (2f,5.5f);
			t = 0f;
			while(t < lerpSpeed) {
				lt.label.Text = ((int)Mathf.Lerp(c, tar, t/lerpSpeed)).ToString();
				t += updateGranularity;
				yield return new WaitForSeconds(updateGranularity);
			}
		}
	}
}
