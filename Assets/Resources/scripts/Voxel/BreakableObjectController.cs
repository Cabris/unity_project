using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

public class BreakableObjectController : MonoBehaviour
{
	[SerializeField]
	GameObject voxelPrototype;
//	[SerializeField]
//	int voxelCount;
	[SerializeField]
	Terrain terrain;
	//Voxel[] voxels;
	[SerializeField]
	public	int maxDivision = 4;
	// Use this for initialization
	void Start ()
	{
		CreateVoxelPrototype();
		//voxelPrototype.SetActive(false);
	}

	void CreateVoxelPrototype(){

		//voxelPrototype=Instantiate(Resources.Load("voxel")) as GameObject;
		//voxelPrototype.transform.parent=terrain.transform;
		voxelPrototype.GetComponent<Voxel>().vControlor=this;
		voxelPrototype.GetComponent<Voxel>().ResetColliderSizeBySprite();
		//return prototype;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//voxelCount=terrain.GetComponentsInChildren<Voxel>().Length;
	}

	public JSONNode GetState(){
		JSONClass stateJ=new JSONClass();
		JSONArray voxelsJ=new JSONArray();
		Voxel[] voxels=terrain.GetComponentsInChildren<Voxel>();
//		voxels=GetComponentsInChildren<Voxel>();
		foreach(Voxel v in voxels){
			if(!v.IsDestorying)
			voxelsJ.Add(FromVoxel(v));
		}
		stateJ.Add("voxels",voxelsJ);
		stateJ.Add("maxDivision",new JSONData(maxDivision));
		return stateJ;
	}

	public JSONClass FromVoxel(Voxel v){
		BreakableObject b=v.gameObject.GetComponent<BreakableObject>();
		BreakableVoxelState state=new BreakableVoxelState(b);
		return state.ToJson();
	}

	public Voxel CreateVoxel(JSONClass goJ){
		Voxel voxel=null;
		BreakableObject bo=null;
		BreakableVoxelState state=new BreakableVoxelState(goJ);
		//voxelPrototype=CreateVoxelPrototype();
		voxel=voxelPrototype.GetComponent<Voxel>().CloneMe(state.spriteRect,state.position,terrain.transform,state.divisionCount);
		bo=voxel.GetComponent<BreakableObject>();
		//voxel.maxDivision=state.divisionMax;
		bo.durableValue=state.durableValue;
		voxel.transform.localScale=state.scale;
		//GameObject.Destroy(voxelPrototype);
		return voxel;
	}


	public void SetState(JSONNode state){
		Voxel[] voxels=terrain.GetComponentsInChildren<Voxel>();
		foreach(Voxel v in voxels){
			GameObject.Destroy(v.gameObject);
		}
		//GameObject.Destroy(voxelPrototype);
		//List<Voxel> vs=new List<Voxel>();
		JSONArray arr=state["voxels"] as JSONArray;
		foreach(JSONNode node in arr){
			CreateVoxel(node as JSONClass);
		}
		maxDivision=state["maxDivision"].AsInt;
		//voxels=vs.ToArray();
	}

}
