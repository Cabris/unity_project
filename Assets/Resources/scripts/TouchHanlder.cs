
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class TouchHanlder : MonoBehaviour {

	public delegate void TouchEventHanlder(MyTouch t);
	public TouchEventHanlder OnTouchEvent; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_ANDROID
		UpdateAnd();
		#elif UNITY_EDITOR
		UpdatePC();
		#endif
	}
	
	void UpdateAnd () {
		// Code for OnMouseDown in the iPhone. Unquote to test.
		RaycastHit hit = new RaycastHit();
		for (int i = 0; i < Input.touchCount; ++i) {
			if(OnTouchEvent!=null)
				OnTouchEvent(new MyTouch(Input.GetTouch(i)));
		}
	}

	void UpdatePC(){
		Vector2 pos=new Vector2( Input.mousePosition.x,Input.mousePosition.y);
		if(Input.GetMouseButtonDown(0)&&OnTouchEvent!=null)
			OnTouchEvent(new MyTouch(0,pos,TouchPhase.Began));
		if(Input.GetMouseButtonUp(0)&&OnTouchEvent!=null)
			OnTouchEvent(new MyTouch(0,pos,TouchPhase.Ended));
		if(Input.GetMouseButton(0)&&OnTouchEvent!=null)
			OnTouchEvent(new MyTouch(0,pos,TouchPhase.Moved));
	}



















}





