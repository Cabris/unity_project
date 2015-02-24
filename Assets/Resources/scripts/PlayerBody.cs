using UnityEngine;
using System.Collections;

public class PlayerBody : MonoBehaviour {
	[SerializeField]
	GameObject damage;
	public float force=5;
	// Use this for initialization
	void Start () {
		GameObject.Find("Scene").GetComponent<TouchHanlder>().OnTouchEvent+=onTouch;
	}

	void OnDestroy() {
		GameObject.Find("Scene").GetComponent<TouchHanlder>().OnTouchEvent-=onTouch;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void onTouch(MyTouch t){
		Ray ray = Camera.main.ScreenPointToRay(t.position);
		RaycastHit hit = new RaycastHit();
		int layerMask=1<<LayerMask.NameToLayer("breakable");
		if (Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask)) {
			if(t.phase.Equals(TouchPhase.Began)){
				Vector3 pos=new Vector3(t.position.x,t.position.y,0);
				Vector3 worldPos= Camera.main.ScreenToWorldPoint(pos);
				Vector3 newPos=new Vector3(worldPos.x,worldPos.y,transform.position.z);
				fire(newPos);
			}
		}
	}

	void fire(Vector3 p){
		Vector3 dir=p-transform.position;
		dir.z=0;
		dir.Normalize();
		StartCoroutine(fireDamage(dir,2.5f));
	}

	IEnumerator fireDamage(Vector3 dir,float s){
		GameObject d=GameObject.Instantiate(damage) as GameObject;
		d.transform.position=transform.position;
		d.rigidbody.AddForce(dir*force);
		yield return new WaitForSeconds(s);
		GameObject.Destroy(d);
	}

}
