using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageSource : MonoBehaviour
{
	[SerializeField]
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

	// Update is called once per frame
	void Update ()
	{
		counter += Time.deltaTime;
		if (counter >= updatePeriod || updatePeriod <= 0) {
			counter = 0;
			if (!isSleep)
				Explose ();
		}
	}
	
	void FixedUpdate ()
	{
		CleanList ();
		foreach (BreakableObject bo in bos) {
			culacPower(bo);
		}
	}

	public void CleanList ()
	{
		List<BreakableObject> tempBos = new List<BreakableObject> ();
		foreach (BreakableObject bo in bos) {
			if (bo != null)
				tempBos.Add (bo);
		}
		bos=tempBos;
	}

	public Vector2 culacPower (BreakableObject target)
	{
		if (target == null){
			Debug.LogWarning("null target");
			return new Vector2();}
		Vector2 targetPos = target.transform.position.ToVector2 ();
		BoxCollider targetCollider = target.GetComponent<BoxCollider> ();
		Vector2 pos = transform.position.ToVector2 ();

		BoxCollider damageCollider = GetComponent<BoxCollider> ();
		Vector2 vect = (targetPos-pos).normalized;

		Rect targetRect = new Rect ();
		Vector2 size=new Vector2(targetCollider.size.x*target.transform.lossyScale.x,
		                         targetCollider.size.y*target.transform.lossyScale.y);
		targetRect.center = targetPos-size/2;
		targetRect.width = size.x;
		targetRect.height = size.y;


		Rect damageRect = new Rect ();
		size=new Vector2(damageCollider.size.x*transform.lossyScale.x,
		                 damageCollider.size.y*transform.lossyScale.y);
		damageRect.center = pos-size/2;
		damageRect.width = size.x;
		damageRect.height = size.y;


		Rect intersectionRect=Extension.intersection (damageRect, targetRect);
		float intersection = intersectionRect.area();
//		Debug.Log ("intersection: " + intersectionRect);
		float minArea = Mathf.Min (damageRect.area(),targetRect.area());
		float factor = intersection / minArea;

		Vector2 power=-vect * factor * maxPower;
		//Debug.Log ("pow: " + power);
		return power;
	}
		
	public void Explose ()
	{
		foreach (BreakableObject bo in bos) {
			if (bo != null) {
				//Debug.Log("Explose");
				bo.Break (this, bo);
			}
		}
	}
	
	void  OnTriggerEnter (Collider other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null && !bos.Contains (bo)){
			bos.Add (bo);
		}
		//CleanList ();
	}
	
	void OnTriggerExit (Collider other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null){
			bos.Remove (bo);
		}
		//CleanList ();
	}
	
}
