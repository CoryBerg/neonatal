using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AttachControlTo3DObject : MonoBehaviour
{
	
	public Camera mainCamera;
	public GameObject attach;
	public Vector3 offset;
	
	private dfControl control;
	
	void OnEnable()
	{
		control = GetComponent<dfControl>();
		if( control != null && mainCamera != null )
		{
			mainCamera.cullingMask |= control.gameObject.layer;
			mainCamera.eventMask &= control.gameObject.layer;
		} 
	}
	
	void Update()
	{
		if( control != null && attach != null )
		{
			control.transform.position = attach.transform.position + offset;
		}
	}
	
}