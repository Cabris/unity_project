using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	bool isTouch;
	public bool isFix; 
	SpringJoint springJoint;
	float spring;
	Vector3 iniPos;
	int fingerId;
	float maxDis=Mathf.Infinity;
	// Use this for initialization
	void Start () {
		isFix=false;
		isTouch=false;
		springJoint=GetComponent<SpringJoint>();
		spring=springJoint.spring;
		GameObject.Find("Scene").GetComponent<TouchHanlder>().OnTouchEvent+=onTouch;
	}

	void OnDestroy() {
		GameObject.Find("Scene").GetComponent<TouchHanlder>().OnTouchEvent-=onTouch;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.isKinematic=isFix||isTouch;
		Debug.DrawRay(ray.origin,ray.direction);
//		if(isTouch)
//			springJoint.spring=0;
//		else
//			springJoint.spring=10;
	}
	
	void OnDown(MyTouch t){
		isTouch=true;
		//Debug.Log(name+" OnMouseDown");
		//springJoint.spring=0;
		if(!isFix)
			iniPos=transform.position;
	}
	
	void OnUp(MyTouch t){
		//Debug.Log(name+" OnMouseUp");
		isTouch=false;
		Vector3 dir;
		dir=transform.position-iniPos;
		Debug.Log(dir.magnitude);
		if(Vector3.Distance(transform.position,iniPos)>1f&&!isFix){
			//if(!isFix)
				Slide(dir);
			Debug.Log("slide");
		}
		else if(isFix){
			Debug.Log("click");
			isFix=false;
		}
		//else 
	}
	
	void OnDrag(MyTouch t){
		if(isTouch&&!isFix){
			Vector3 pos=new Vector3(t.position.x,t.position.y,0);
			Vector3 worldPos= Camera.main.ScreenToWorldPoint(pos);
			Vector3 newPos=new Vector3(worldPos.x,worldPos.y,transform.position.z);
			if(Vector3.Distance(newPos,iniPos)<=maxDis)
				transform.position=newPos;
//			else{
//				Vector3 v=(newPos-iniPos).normalized;
//				Vector3 p=iniPos+v*maxDis;
//				p=Vector3.zero;
//				transform.position=p;
//			}
			//springJoint.spring=0;
		}
	}
	

	
	Ray ray;
	void Slide(Vector3 v){
		Debug.Log("v:"+v);
		RaycastHit hit;
		int layerMask=1<<LayerMask.NameToLayer("breakable");
		ray=new Ray(iniPos,v.normalized);
		if(Physics.Raycast(ray,out hit,maxDis,layerMask)){
			Debug.Log("hit: "+hit.point);
			Vector3 pos=new Vector3(hit.point.x,hit.point.y,transform.position.z);
			transform.position=pos;
			isFix=true;
		}else{
			transform.position=Vector3.zero;
		}
	}
	
	void onTouch(MyTouch t){
		
		Ray ray = Camera.main.ScreenPointToRay(t.position);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit)) {
			if(hit.collider.gameObject==gameObject){
				if(t.phase.Equals(TouchPhase.Began)){
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






























