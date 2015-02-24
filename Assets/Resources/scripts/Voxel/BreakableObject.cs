using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour {

	public float durableValue;
	//public float hardness;
	Voxel voxel;
	// Use this for initialization
	void Start () {
		voxel=GetComponent<Voxel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Break(DamageSource exp,BreakableObject bo){
		BreakableObject[] bos=DoDamage(exp,bo);
		if(bos.Length==0)
			return;
		foreach(BreakableObject b in bos){
			if(bo!=null)
				b.Break(exp,bo);
		}
	}


	public  BreakableObject[] DoDamage(DamageSource exp,BreakableObject bo){
		BreakableObject[] bos=new BreakableObject[0];
		Vector2 power=exp.culacPower(bo);
		float m=power.magnitude;
		if(m >0){
			bo.durableValue-=m;
			if(bo.durableValue<=0){
				bo.voxel.Disintegration();
				//GameObject.Destroy(bo.gameObject);
			}else if(!bo.voxel.IsDestorying)
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
