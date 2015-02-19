
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class OnTouch : MonoBehaviour {

	public delegate void TouchEventHanlder(Touch t);
	public static TouchEventHanlder OnTouchEvent; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_ANDROID
		UpdateAnd();
		#endif
	}
	
	void UpdateAnd () {
		// Code for OnMouseDown in the iPhone. Unquote to test.
		RaycastHit hit = new RaycastHit();
		for (int i = 0; i < Input.touchCount; ++i) {
			if(OnTouchEvent!=null)
				OnTouchEvent(Input.GetTouch(i));
//			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
//				// Construct a ray from the current touch coordinates
//				Touch t=Input.GetTouch(i);
//				Ray ray = Camera.main.ScreenPointToRay(t.position);
//				if (Physics.Raycast(ray, out hit)) {
//					hit.transform.gameObject.SendMessage("OnMouseDown",t.fingerId);
//				}
//			}

//			if (Input.GetTouch(i).phase.Equals(TouchPhase.Moved)) {
//				// Construct a ray from the current touch coordinates
//				Touch t=Input.GetTouch(i);
//				Ray ray = Camera.main.ScreenPointToRay(t.position);
//				if (Physics.Raycast(ray, out hit)) {
//					hit.transform.gameObject.SendMessage("OnMouseDrag",t.fingerId);
//				}
//			}

//			if (Input.GetTouch(i).phase.Equals(TouchPhase.Ended)) {
//				// Construct a ray from the current touch coordinates
//				Touch t=Input.GetTouch(i);
//				Ray ray = Camera.main.ScreenPointToRay(t.position);
//				if (Physics.Raycast(ray, out hit)) {
//					hit.transform.gameObject.SendMessage("OnMouseUp",t.fingerId);
//				}
//			}
		}
		
	}




}





