using UnityEngine;
using System.Collections;
 
public class Dragable : MonoBehaviour
{
	public bool isMouseDown;
	public bool enableMove;
	public bool isFixOffset{ get; set; }

	public Vector2 fixOffSet = new Vector2 ();
	private Camera cam;
	private Transform myTransform  ;
	private Vector3 position;
	Vector3 offSet;
	Vector3 mousePos ;
	
	void Start ()
	{
		isMouseDown = false;
		myTransform = this.transform;
		position = myTransform.position;
		offSet = Vector3.zero;
		isFixOffset = false;
		cam = Camera.main;
		if (!cam) {
			Debug.LogError ("Can't find camera tagged MainCamera");
			return;
		}
	}

	void OnMouseDown ()
	{
		isMouseDown = true;
		position = myTransform.position;
		offSet = position - cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 0));
		offSet.z = 0;
	}
 
	void OnMouseUp ()
	{	
		isMouseDown = false;	
	}
	
	void FixedUpdate ()
	{
		//rigidbody.isKinematic=isMouseDown;
		mousePos = Input.mousePosition;
		Vector3 mousePos_scene = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, 0));
		mousePos_scene.z = 0;
		Vector3 move = mousePos_scene - position;
		move.z = 0;
		move += offSet;
		if (isMouseDown && enableMove) {
			if (isFixOffset)
				myTransform.position = mousePos_scene + new Vector3 (fixOffSet.x, fixOffSet.y, 0);
			else
				myTransform.position = position + move;
		}
	}
}