using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
	public	List<BreakableObject> bos;
	public GameObject button;
	CircleCollider2D c;
	public float maxPower;

	// Use this for initialization
	void Start ()
	{
		UIEventListener.Get (button).onClick += ButtonClick;
		 c = GetComponent<CircleCollider2D>();
	}

	List<BreakableObject> NewMethod ()
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
		var tempBos = NewMethod ();
		bos=tempBos;
	}
	
	public Vector2 culacPower (Vector2 pos)
	{
		float radius=c.radius*transform.localScale.x;
		Vector2 p = transform.position.ToVector2();
		float d = Vector2.Distance (pos, p);
		float angle=Mathf.Atan2(pos.y-p.y,pos.x-p.x);
		float factor=(radius*radius)/(d*d);
		if(d>=radius)
			return new Vector2();
		Vector2 power=new Vector2();
		power.x=maxPower*Mathf.Cos(angle)*factor;
		power.y=maxPower*Mathf.Sin(angle)*factor;
		return power;
	}
	
	void ButtonClick (GameObject button)
	{
		Debug.Log ("GameObject " + button.name);



		foreach (BreakableObject bo in NewMethod()) {
			Debug.Log (culacPower (bo.transform.position.ToVector2 ()));
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
