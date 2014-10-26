using UnityEngine;
using System.Collections;

public class EraseBrush : BaseBrush
{
	DamageSource damage;
	BaseBrush brush;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		damage = GetComponentInChildren<DamageSource> ();
		brush=GetComponent<BaseBrush>();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		damage.isSleep=!brush.IsActive;
	}

}














