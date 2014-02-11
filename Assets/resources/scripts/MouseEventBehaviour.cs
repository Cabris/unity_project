using UnityEngine;
using System.Collections;

public class MouseEventBehaviour : MonoBehaviour {
	
	public bool isMouseDown;
	
	public delegate void MouseEvent();
	public MouseEvent OnMouseDownEvent,OnMouseUpEvent;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown ()
	{
		isMouseDown=true;
		if(OnMouseDownEvent!=null)
			OnMouseDownEvent();
	}
 
	void OnMouseUp ()
	{
		isMouseDown=false;
		if(OnMouseUpEvent!=null)
			OnMouseUpEvent();
	}
	
}
