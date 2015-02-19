using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	bool isTouch;
	public bool isFix; 
	SpringJoint springJoint;
	float spring;
	Vector3 iniPos;
	int fingerId;
	// Use this for initialization
	void Start () {
		isFix=false;
		isTouch=false;
		springJoint=GetComponent<SpringJoint>();
		spring=springJoint.spring;
		OnTouch.OnTouchEvent+=onTouch;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.isKinematic=isFix;
		//if(ray!=null)
//		if(isTouch)
//			OnDrag();
		Debug.DrawRay(ray.origin,ray.direction);
	}
	
	void OnDown(Touch t){
		iniPos=transform.position;
		//Debug.Log(name+" OnMouseDown");
		isTouch=true;
		//springJoint.spring=0;
	}
	
	void OnUp(Touch t){
		//Debug.Log(name+" OnMouseUp");
		isTouch=false;
		Vector3 dir=new Vector3(t.deltaPosition.x,t.deltaPosition.y,0);
		dir=transform.position-iniPos;
		Debug.Log(dir.magnitude);
		if(dir.magnitude>.1f){
			if(!isFix)
				Slide(dir);
			Debug.Log("slide");
		}
		else{
			Debug.Log("click");
			isFix=false;
		}
	}
	
	void OnDrag(Touch t){
		if(isTouch&&!isFix){
			Vector3 pos=new Vector3(t.position.x,t.position.y,0);
			Vector3 worldPos= Camera.main.ScreenToWorldPoint(pos);
			Vector3 newPos=new Vector3(worldPos.x,worldPos.y,transform.position.z);
			transform.position=newPos;
			//springJoint.spring=0;
		}
		//else
		//	springJoint.spring=spring;
	}
	
	//#if UNITY_ANDROID

//	void OnMouseDown(int fId){
//		fingerId=fId;
//		OnDown();
//	}
//	
//	void OnMouseUp(int fId){
//		OnUp();
//	}
//	
//	void OnMouseDrag(int fId){
//		if(fId==fingerId)
//			OnDrag();
//	}
//	#endif
	
	Ray ray;
	void Slide(Vector3 v){
		Debug.Log("v:"+v);
		RaycastHit hit;
		int layerMask=1<<10;
		ray=new Ray(iniPos,v.normalized);
		if(Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask)){
			Debug.Log("hit: "+hit.point);
			transform.position=hit.point;
			isFix=true;
		}else{
			transform.position=iniPos;
		}
	}
	
	void onTouch(Touch t){
		if(t.phase.Equals(TouchPhase.Began)){
			Ray ray = Camera.main.ScreenPointToRay(t.position);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit)) {
				if(hit.collider.gameObject==gameObject){
					fingerId=t.fingerId;
					OnDown(t);
				}
			}
		}
		if(isTouch&&t.phase.Equals(TouchPhase.Ended)&&t.fingerId==fingerId){
			OnUp(t);
		}
		if(isTouch&&t.phase.Equals(TouchPhase.Moved)&&t.fingerId==fingerId)
			OnDrag(t);
	}
	
}






























