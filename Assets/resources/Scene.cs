using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour
{
	Camera cam;
	[SerializeField]
	Wall wall;
	[SerializeField]
	Body aBody;
	[SerializeField]
	MyTouch restartButton;
	// Use this for initialization
	void Start ()
	{
		//wall.OnWallDown += this.onWallDown;
		//restartButton.OnMouseDownEvent+=this.onRestart;
		cam = Camera.main;
	}
	
	void onWallDown (Vector3 pos)
	{
		//Debug.Log ("g:" + pos);
		
		Vector3 direct = pos - aBody.transform.position;
		direct.Normalize ();
		
		RaycastHit hit;
		if (Physics.Raycast (aBody.transform.position, direct, out hit, Mathf.Infinity, 1<<LayerMask.NameToLayer ("wall"))) {
			Debug.Log (hit.collider.tag);
			if (hit.collider.tag == "wall")
				aBody.HandGoTo (hit.point);
		}
	}
	
	void OnMouseDown ()
	{
		Vector3 pos = cam.ScreenToWorldPoint (Input.mousePosition);
		this.onWallDown(pos);
	}
	
	void onRestart(){
		aBody.ReleaseHandLock();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
