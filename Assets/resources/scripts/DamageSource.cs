using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageSource : MonoBehaviour
{
	List<BreakableObject> bos;
	public float maxPower;
	public float updatePeriod;
	public bool isSleep=false;
	float counter = 0;
	
	// Use this for initialization
	void Start ()
	{
		//UIEventListener.Get (button).onClick += ButtonClick;

		bos=new List<BreakableObject>();
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
		bos = CleanList ();	
		counter += Time.deltaTime;
		if (counter >= updatePeriod || updatePeriod <= 0) {
			counter = 0;
			if (bos.Count > 0&&!isSleep)
				Explose ();
		}
		
	}
	
	void FixedUpdate ()
	{
		
	}
	
	public Vector2 culacPower (BreakableObject target)
	{
		//CircleCollider2D cCollider2D = GetComponent<CircleCollider2D> ();
		//float radius = cCollider2D.radius * transform.localScale.x;
		//		LayerMask mask = bc.gameObject.layer;
		//		RaycastHit2D[] hits = Extension.CircleScan (p, radius, mask, bc);

		if (target == null)
			return new Vector2();
		Vector2 targetPos = target.transform.position.ToVector2 ();
		BoxCollider2D targetCollider = target.GetComponent<BoxCollider2D> ();
		Vector2 pos = transform.position.ToVector2 ();

		BoxCollider2D damageArea = GetComponent<BoxCollider2D> ();
		Vector2 vect = (targetPos-pos).normalized;

		Rect targetRect = new Rect ();
		targetRect.center = targetPos;
		targetRect.width = targetCollider.size.x;
		targetRect.height = targetCollider.size.y;

		Rect damageRect = new Rect ();
		damageRect.center = pos;
		damageRect.width = damageArea.size.x;
		damageRect.height = damageArea.size.y;

		float intersection = Extension.intersection (damageRect, targetRect).area();
		if(intersection==0)
			return new Vector2 ();
		float minArea = Mathf.Min (damageRect.area(),targetRect.area());
		float factor = intersection / minArea;

		Vector2 power=-vect * factor * maxPower;
//		Debug.Log ("pow: " + power);
		return power;
		
//		if (hits.Length == 0)
//			return new Vector2 ();
//		Vector2 power = new Vector2 ();
//		foreach (RaycastHit2D hit in hits) {
//			//Debug.Log( hit.fraction);
//			float t = 1 - hit.fraction;
//			power += hit.normal * t;
//		}
//		if (bc.OverlapPoint (p))
//			return new Vector2 (maxPower, maxPower);
//		power.Normalize ();	
//		float d = Vector2.Distance (boP, p);
//		float factor = (radius * radius) / (d * d);
//		return -power * factor * maxPower;
	}
		
	public void Explose ()
	{
		foreach (BreakableObject bo in CleanList()) {
			if (bo != null) {
				bo.Break (this, bo);
			}
		}
	}
	
	void  OnTriggerEnter2D (Collider2D other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null && !bos.Contains (bo))
			bos.Add (bo);
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null)
			bos.Remove (bo);
	}
	
}
