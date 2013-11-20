using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour
{
	
	[SerializeField]
	Wall wall;
	[SerializeField]
	Body aBody;
	// Use this for initialization
	void Start ()
	{
		wall.OnWallDown += this.onWallDown;
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
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
