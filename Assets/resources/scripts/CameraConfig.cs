using UnityEngine;
using System.Collections;

public class CameraConfig : MonoBehaviour {
	[SerializeField]
	Camera mainCamera;
	// Use this for initialization
	[SerializeField]
	float gameTargetHeight;
	[SerializeField]
	float pixelsToUnits;
	static CameraConfig config;

	void Start () {
		//mainCamera=Camera.main;
		mainCamera.orthographic=true;
		config=this;
		mainCamera.orthographicSize=(gameTargetHeight / 2) / pixelsToUnits;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
			mainCamera.orthographicSize=(gameTargetHeight / 2) / pixelsToUnits;
		if(Input.GetKey(KeyCode.KeypadPlus))
			mainCamera.orthographicSize-=.1f;
		if(Input.GetKey(KeyCode.KeypadMinus))
			mainCamera.orthographicSize+=.1f;
	}

	public static CameraConfig Singleten{
		get{
			return config;}
	}

	public float PixelsToUnits{get{return pixelsToUnits;}}

}
