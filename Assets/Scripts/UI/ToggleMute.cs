using UnityEngine;
using System.Collections;

public class ToggleMute : MonoBehaviour {
	public dfButton button;
	private int muted = 0;
	// Use this for initialization
	void Start () {
		if(button == null) {
			button = this.GetComponent<dfButton>();
		}
		if(PlayerPrefs.HasKey("muteAudio")) {
			muted = PlayerPrefs.GetInt("muteAudio");
		}
		SetMute();
	}

	void SetMute() {
		if(muted == 1) {
			AudioListener.volume = 0;
			button.Text = "Unmute";
		} else {
			AudioListener.volume = 1;
			button.Text = "Mute";
		}
	}

	void MuteToggle() {
		muted = Mathf.Abs(muted - 1);
		PlayerPrefs.SetInt("muteAudio",muted);
		SetMute();
	}
	
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent ) {
		MuteToggle();
	}
}
