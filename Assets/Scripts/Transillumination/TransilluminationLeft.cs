using UnityEngine;
using System.Collections;

public class TransilluminationLeft : MonoBehaviour {
	
	public Transform target ;
	public float damping = 6;
	public bool isTriggered = false;
	public bool beginProcessBegin = false;
	public bool endProcessBegin = false;

	public float zoom = 9f;
	public float normal = 58.1f;
	public float smooth = 10f;
	public Camera cameraMain ;
	public Camera cameraBlip ;

	public Light light1, light2;
	private float oneDefaultIntensity, twoDefaultIntensity, blipDefaulyFocus;

	public float delay = 3f;

	public GameObject flashLight;
	
	void Awake () {
		oneDefaultIntensity = light1.intensity;
		twoDefaultIntensity = light2.intensity;
		blipDefaulyFocus = cameraBlip.fieldOfView;
	}

	public void BeginToggle() {
		beginProcessBegin = !beginProcessBegin;
		print (beginProcessBegin);
		if (beginProcessBegin) {
			StartCoroutine (BeginProcess(delay));
		} 
	}

	public void CamToggle() {
		isTriggered = !isTriggered;
		print (isTriggered);
		if (isTriggered) {
			StartCoroutine (OpperationProcess(delay));
		} 
	}

	public void EndToggle(){
		endProcessBegin = !endProcessBegin;
		print (endProcessBegin);
		if (endProcessBegin) {
			StartCoroutine (EndProcess(delay));		
		}

	}
	

	void CamAnimate() {
		this.animation.Play ("transilluminateCamZoom");
	}
	
	IEnumerator BeginProcess (float delay) {
		
		//Step 1: Camera Zoom In
		print ("Going in");
		this.animation ["transilluminateCamZoom"].speed = 1f;
		CamAnimate ();
		
		cameraBlip.fieldOfView = zoom;
		
		//step 2: Lights go out
		
		light1.intensity = 0.05f;
		light2.intensity = 0.05f;
		yield return new WaitForSeconds(1);
		
		
		beginProcessBegin = false;
	}

	IEnumerator OpperationProcess (float delay) {

		//Step 3: Flash Light Comes In
		yield return new WaitForSeconds(1);
		flashLight.animation ["flashLightFlyIn"].speed = 1f;
		flashLight.animation.Play("flashLightFlyIn");


		yield return new WaitForSeconds(delay);


		//Step 4: Flash Light Flys Out
		flashLight.animation ["flashLightFlyIn"].speed = -1f;
		flashLight.animation.Play("flashLightFlyIn");
		yield return new WaitForSeconds(0.25f);


		isTriggered = false;
	}

	IEnumerator EndProcess (float delay) {

		//Step 5: Camera rows back
		
		print ("Going out");
		this.animation ["transilluminateCamZoom"].speed = -.5f;
		CamAnimate ();
		
		cameraBlip.fieldOfView = blipDefaulyFocus;
		
		//Step 6: Lghits go back on
		
		light1.intensity = oneDefaultIntensity;
		light2.intensity = twoDefaultIntensity;
		
		endProcessBegin = false;
		yield return new WaitForSeconds(0.25f);
	}

	void Update () {

		}

	

}