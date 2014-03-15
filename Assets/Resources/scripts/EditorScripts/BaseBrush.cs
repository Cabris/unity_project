using UnityEngine;
using System.Collections;

public class BaseBrush : MonoBehaviour {

	public bool IsActive{ 
		get;
		set;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetMouseButtonDown(0)){
			IsActive=true;
		}
		if(Input.GetMouseButtonUp(0)){
			IsActive=false;
		}
		//		if (Input.GetMouseButtonDown(1))
		//		
		//		if (Input.GetMouseButtonDown(2))
		
		float wheelValue=Input.GetAxis("Mouse ScrollWheel");
		SetBrushSize(wheelValue);
		Vector3 mousePosInWorld=Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position=mousePosInWorld;
		
	}
	
	void SetBrushSize(float sizeMulp){
		Vector3 scale=transform.localScale;
		Vector3 max=new Vector3(1,1,1);
		Vector3 min=new Vector3(0.05f,0.05f,0.05f);
		Vector3 dv=new Vector3(0.5f,0.5f,0.5f);
		scale+=dv*sizeMulp;
		
		if(scale.x>min.x&&scale.x<max.x&&
		   scale.y>min.y&&scale.y<max.y&&
		   scale.z>min.z&&scale.z<max.z){
			transform.localScale=scale;
		}
	}
}
