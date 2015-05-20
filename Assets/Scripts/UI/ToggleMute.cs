using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleMute : MonoBehaviour {

	private int muted = 0;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("muteAudio")) {
			muted = PlayerPrefs.GetInt("muteAudio");
		}
		SetMute();
	}

	void SetMute() {
		if(muted == 1) {
			AudioListener.volume = 0;
			this.gameObject.GetComponentInChildren<Text> ().text = "Unmute";
		} else {
			AudioListener.volume = 1;
			this.gameObject.GetComponentInChildren<Text> ().text = "Mute";
		}
	}

	public void MuteToggle() {
		muted = Mathf.Abs(muted - 1);
		PlayerPrefs.SetInt("muteAudio",muted);
		SetMute();
	}
}
