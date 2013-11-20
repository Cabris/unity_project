using UnityEngine;
using System.Collections;

public class MyTouch : MonoBehaviour {

	public delegate void touchEvent();
	public touchEvent OnMouseDownEvent;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown ()
	{
	if(OnMouseDownEvent!=null)
			OnMouseDownEvent();
	}
	
	void OnMouseUp ()
	{
		
	}
}
