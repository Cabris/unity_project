using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
	float spring;
	float damper;
	RigidbodyConstraints free, locked;
	Uni2DSprite sprite;
	Color32 linkColor, freeColor;
	bool isLock;
	Transform bodyT;
	// Use this for initialization
	void Start ()
	{
		free = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		locked = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
		sprite = this.GetComponent<Uni2DSprite> ();	
		linkColor = new Color32 (255, 255, 255, 255);
		freeColor = new Color32 (255, 255, 255, 128);
		IsLink = true;
		isLock = false;
		bodyT=this.GetComponent<SpringJoint> ().connectedBody.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (IsLink) {
			this.GetComponent<SpringJoint> ().spring = this.spring;
			this.GetComponent<SpringJoint> ().damper = this.damper;
			
			Color32 c=sprite.VertexColor;
			c.a=linkColor.a;
			sprite.VertexColor=c;
		} else {
			this.GetComponent<SpringJoint> ().spring = 0;
			this.GetComponent<SpringJoint> ().damper = 0;
			Color32 c=sprite.VertexColor;
			c.a=freeColor.a;
			sprite.VertexColor=c;
		}
	}
	
	public void LockHand (Vector3 pos)
	{
		Vector3 _pos = transform.position;
		_pos.x = pos.x;
		_pos.y = pos.y;
		transform.position = _pos;
		rigidbody.constraints = locked;
		isLock = true;
		transform.parent=null;
	}
	
	public void FreeHand ()
	{
		rigidbody.constraints = free;
		isLock = false;
		transform.parent=bodyT;
	}
	
	public void SetSpring (float spring)
	{
		this.spring = spring;
	}
	
	public void SetDamper (float damper)
	{
		this.damper = damper;
	}
	
	public bool IsLink{ get; set; }
	
	void OnMouseDown ()
	{
		if (isLock)
			IsLink = !IsLink;
	}
	
}
