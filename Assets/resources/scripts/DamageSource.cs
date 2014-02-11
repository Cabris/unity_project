using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageSource : MonoBehaviour
{
	public	List<BreakableObject> bos;
	//public GameObject button;
	CircleCollider2D c;
	public float maxPower;
	public float updatePeriod;
	float counter=0;

	// Use this for initialization
	void Start ()
	{
		//UIEventListener.Get (button).onClick += ButtonClick;
		 c = GetComponent<CircleCollider2D>();
	}

	List<BreakableObject> CleanList ()
	{
		List<BreakableObject> tempBos = new List<BreakableObject> ();
		foreach (BreakableObject bo in bos) {
			if (bo != null)
				tempBos.Add (bo);
		}
		return tempBos;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var tempBos = CleanList ();
		bos=tempBos;

		counter+=Time.deltaTime;
		if(counter>=updatePeriod||updatePeriod<=0){
			counter=0;
			if(bos.Count>0)
				Explose();
		}

	}

	void FixedUpdate(){

	}
	
	public Vector2 culacPower (BreakableObject bo)
	{
		Vector2 boP=bo.transform.position.ToVector2();
		float radius=c.radius*transform.localScale.x;
		Vector2 p = transform.position.ToVector2();

		BoxCollider2D bc=bo.GetComponent<BoxCollider2D>();
		LayerMask mask=bc.gameObject.layer;
		RaycastHit2D[] hits= Extension.CircleScan(p,radius,mask,bc);
		if(hits.Length==0)
			return new Vector2();
		Vector2 power=new Vector2();
		foreach(RaycastHit2D hit in hits){
			//Debug.Log( hit.fraction);
			float t=1-hit.fraction;
			power+=hit.normal*t;
		}
		if(bc.OverlapPoint(p))
			return new Vector2(maxPower,maxPower);
		power.Normalize();	
		float d = Vector2.Distance (boP, p);
		float factor=(radius*radius)/(d*d);
		return -power*factor*maxPower;
	}
	
	void ButtonClick (GameObject button)
	{
		Debug.Log ("GameObject " + button.name);
		Explose();
	}

	public void Explose(){
		foreach (BreakableObject bo in CleanList()) {
			if(bo!=null){
				bo.Break(this,bo);
			}
		}
	} 

	void  OnTriggerStay2D (Collider2D other)
	{
//		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
//		if (bo != null)
//		bo.Break(this,bo);
	}

	void  OnTriggerEnter2D (Collider2D other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null&&!bos.Contains(bo))
			bos.Add (bo);
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null)
			bos.Remove (bo);
	}
	
}
