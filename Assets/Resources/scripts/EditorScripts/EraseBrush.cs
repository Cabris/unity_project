using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BaseBrush))]
public class EraseBrush : MonoBehaviour
{
	DamageSource damage;
	BaseBrush brush;

	// Use this for initialization
	void Start ()
	{
		damage = GetComponentInChildren<DamageSource> ();
		brush=GetComponent<BaseBrush>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		damage.isSleep=!brush.IsActive;
	}

}














