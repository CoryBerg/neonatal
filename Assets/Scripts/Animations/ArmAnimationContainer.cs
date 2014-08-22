using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmAnimationContainer {
	private Dictionary<string, string> animations;

	// Builds dictionary of ButtonPressed:AnimationName
	public ArmAnimationContainer() {
		animations = new Dictionary<string, string> ()
			{
				{"ButtonNeedle", "NeedleDecomp"},
				{"ButtonSteth", "UseStethoscope"},
				{"ButtonChest", "ChestCompression"},
				{"ButtonSunctionbaby", "Sunction"},
				{"ButtonIntubation", "Intubation"},
				{"StartCC", "StartCC"},
				{"EndCC", "EndCC"},
				{"Stethescope", "UseStethoscope"} // Unnecessary
			};
	}

	public string GetAnimation(string key) {
		if(animations.ContainsKey(key)) {
			return animations[key];
		} else {
			return "";
		}
	}
}
