using UnityEngine;
using System.Collections;

public class TransilluminationRight : MonoBehaviour {

	public Transform target ;
	public float damping = 6;
	public bool isTriggered = false;
	
	public float zoom = 9f;
	public float normal = 58.1f;
	public float smooth = 10f;
	public Camera cameraMain ;
	public Camera cameraBlip ;
	
	public Light light1, light2;
	
	public float delay = 4f;
	
	public GameObject flashLight;
	
	void Awake () {
	}
	
	public void CamToggle() {
		isTriggered = !isTriggered;
		print (isTriggered);
		if (isTriggered) {
			StartCoroutine (Process(delay));
		} 
	}
	
	
	void CamAnimate() {
		this.animation.Play ("transilluminateCamZoom");
	}
	
	
	
	IEnumerator Process (float delay) {
		
//		//Step 1: Camera Zoom In
//		print ("Going in");
//		this.animation ["transilluminateCamZoom"].speed = 1f;
//		CamAnimate ();
//		
//		cameraBlip.fieldOfView = zoom;
//		
//		//step 2: Lights go out
//		
//		light1.intensity = 0.05f;
//		light2.intensity = 0.05f;
		
		//Step 3: Flash Light Comes In
		yield return new WaitForSeconds(1);
		flashLight.animation ["flashLightFlyInR"].speed = 1f;
		flashLight.animation.Play("flashLightFlyInR");
		
		
		yield return new WaitForSeconds(delay);
		
		
		//Step 4: Flash Light Flys Out
		flashLight.animation ["flashLightFlyInR"].speed = -1f;
		flashLight.animation.Play("flashLightFlyInR");
		yield return new WaitForSeconds(0.25f);
//		
//		//Step 5: Camera rows back
//		
//		print ("Going out");
//		this.animation ["transilluminateCamZoom"].speed = -.5f;
//		CamAnimate ();
//		
//		cameraBlip.fieldOfView = blipDefaulyFocus;
//		
//		//Step 6: Lghits go back on
//		
//		light1.intensity = oneDefaultIntensity;
//		light2.intensity = twoDefaultIntensity;
		
		isTriggered = false;
	}
	
	void Update () {
		
	}
	

}
