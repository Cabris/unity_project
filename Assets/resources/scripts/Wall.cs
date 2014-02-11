using UnityEngine;
using System.Collections;
using System;

public class Wall : MonoBehaviour
{
	
	public delegate void onWallTouchEvent (Vector3 pos);

	public onWallTouchEvent OnWallDown;
	Camera cam;
	// Use this for initialization
	void Start ()
	{
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnMouseDown ()
	{
		Vector3 pos = cam.ScreenToWorldPoint (Input.mousePosition);
	//	Vector2 pos2 = new Vector2 (pos.x, pos.y);
		if (OnWallDown != null)
			OnWallDown (pos);
	}
	
	void OnMouseUp ()
	{
		
	}
}
