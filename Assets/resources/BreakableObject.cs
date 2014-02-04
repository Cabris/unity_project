using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour {

	public float durableValue;
	//public float hardness;
	public Voxel voxel;
	// Use this for initialization
	void Start () {
		voxel=GetComponent<Voxel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Break(Explosion exp,BreakableObject bo){
		BreakableObject[] bos=BreakableObject.DoDamage(exp,bo);
		if(bos==null)
			return;
		foreach(BreakableObject b in bos){
			b.Break(exp,bo);
		}
	}


	public static BreakableObject[] DoDamage(Explosion exp,BreakableObject bo){
		BreakableObject[] bos=null;
		Vector2 power=exp.culacPower(bo.transform.position.ToVector2());
		float m=power.magnitude;
		if(m >0){
			bo.durableValue-=m;
			if(bo.durableValue<=0){
				GameObject.Destroy(bo.gameObject);
				bos=new BreakableObject[0];
			}else
			{
				Voxel[] voxs=bo.voxel.Break(2,2);
				bos=new BreakableObject[voxs.Length];
				for(int i=0;i<voxs.Length;i++){
					bos[i]=voxs[i].GetComponent<BreakableObject>();
				}
			}
		}
		return bos;
	}

}
