using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BabyAnimationContainer {
	public static float ANIM_SENTINEL = 1000f;
	private Dictionary<string, float> animations;

	// Builds dictionary of ButtonPressed:AnimationName
	public BabyAnimationContainer() {
		animations = new Dictionary<string, float> ()
			{
				{"Idle", 0.0f},
				{"ButtonNeedle", 0.0f},
				{"ButtonSteth", -1.0f},
				{"ButtonChest", -1.0f},
				{"ButtonSuction", 0.0f},
				{"ButtonIntubation", 1.0f},
			};
	}

	public float GetAnimation(string key) {
		if(animations.ContainsKey(key)) {
			return animations[key];
		}
		return ANIM_SENTINEL; // Sentinel value for trigger animation, this means the animation doesn't exist
	}
}
