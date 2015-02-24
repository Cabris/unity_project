using UnityEngine;
using System.Collections;

public class EraseBrush : BaseBrush
{
	DamageSource damage;
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		damage = GetComponentInChildren<DamageSource> ();
	}
	
	// Update is called once per frame
	protected override void myUpdate ()
	{
		base.myUpdate ();
		IsActive=Input.GetMouseButton(0);
		damage.isSleep = !IsActive;
	}
	
	protected override void updateBrushSize (float d)
	{
		base.updateBrushSize (d);
		BoxCollider collider = damage.GetComponent<BoxCollider> ();
		Bounds b= spriteRenderer.bounds;
		Extension.ResetColliderSizeBySprite (b,collider);
	}
	
	//	protected override void SetBrushSize (float sizeMulp)
	//	{
	//		Vector3 scale=transform.localScale;
	//		Vector3 max=new Vector3(1,1,1);
	//		Vector3 min=new Vector3(0.05f,0.05f,0.05f);
	//		Vector3 dv=new Vector3(0.5f,0.5f,0.5f);
	//		scale+=dv*sizeMulp;
	//		
	//		if(scale.x>min.x&&scale.x<max.x&&
	//		   scale.y>min.y&&scale.y<max.y&&
	//		   scale.z>min.z&&scale.z<max.z){
	//			damage.transform.localScale=scale;
	//		}
	//	}
	
}














