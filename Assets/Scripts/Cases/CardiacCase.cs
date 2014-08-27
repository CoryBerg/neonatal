using UnityEngine;
using System.Collections;

public class CardiacCase : RespiratoryCase {
	// States 
	// 1 = furtherdecomp
	// 2 = win
	// 3 = death
	
	protected override void Awake() {
		base.Awake();
		decompTimer = 600f;
		deathTimer = 900f;
	}

	protected override void InitialState ()	{
		temperature = "37.1";
		Sp02 = "75%";
		bloodPressure = "50/25";
		heartRate = "180";
		bpm = 180;
		UpdateMonitor();
		babyBody = GameObject.FindGameObjectWithTag("BabyBody");
		babyMaterial = babyBody.renderer.material;
		babyMaterial.SetFloat ("_Blend", 0.0f);
	}

	protected override void BabyDeath () {
		temperature = "37.1";
		currentState = 3; // death
		Sp02 = "85%";
		bloodPressure = "65/35";
		heartRate = "140";
		bpm = 140;
		// Pulse strength strong
		babyMaterial.SetFloat ("_Blend", 1.0f);
		UpdateMonitor();
	}

	protected override void BabyRecovery ()	{
		temperature = "37.1";
		currentState = 2; // win
		Sp02 = "85%";
		bloodPressure = "65/35";
		heartRate = "140";
		bpm = 140;
		// strong pulse
		// cyanosis disabled
		Invoke ("ChangeScene", 3.0f);
		babyMaterial.SetFloat ("_Blend", 0.0f);
		UpdateMonitor();
	}

	// After 20 minutes without prostaglandin drip
	protected override void FurtherDecomp () {
		temperature = "37.1";
		currentState = 1;
		Sp02 = "60%";
		// Cyanosis enabled
		// resp rate: 120 b/m
		bloodPressure = "30/10";
		heartRate = "220";
		bpm = 220;
		babyMaterial.SetFloat ("_Blend", 1.0f);
		UpdateMonitor();
		// weak pulse strength
	}
}
