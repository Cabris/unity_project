using UnityEngine;
using System.Collections;

public class CameraConfig : MonoBehaviour {
	Camera mainCamera;
	// Use this for initialization
	[SerializeField]
	float gameTargetHeight;
	[SerializeField]
	float pixelsToUnits;
	static CameraConfig config;

	void Start () {
		mainCamera=Camera.main;
		mainCamera.orthographic=true;
		config=this;
	}
	
	// Update is called once per frame
	void Update () {
		mainCamera.orthographicSize=(gameTargetHeight / 2) / pixelsToUnits;
	}

	public static CameraConfig Singleten{
		get{
			return config;}
	}

	public float PixelsToUnits{get{return pixelsToUnits;}}

}
