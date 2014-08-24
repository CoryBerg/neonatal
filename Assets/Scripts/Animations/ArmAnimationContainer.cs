using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmAnimationContainer {
	private List<string> animations;

	// Builds list of existing animations
	public ArmAnimationContainer() {
		animations = new List<string> () {
			"ButtonChest",
				"StartCC",
				"EndCC",
			"ButtonIntubation",
			"ButtonNeedle",
			"ButtonSuction"
		};
	}

	public string GetAnimation(string anim) {
		if(animations.Contains(anim)) {
			return anim;
		} else {
			 Debug.Log("Animation not found");
			return "";
		}
	}
}
