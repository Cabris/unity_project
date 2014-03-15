using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

public class BreakableObjectController : MonoBehaviour
{
	GameObject voxelPrototype;
	[SerializeField]
	int voxelCount;
	[SerializeField]
	Terrain terrain;
	Voxel[] voxels;
	// Use this for initialization
	void Start ()
	{
		voxelPrototype=CreateVoxelPrototype();
		//voxelPrototype.SetActive(false);
	}

	GameObject CreateVoxelPrototype(){
		GameObject prototype;
		prototype=Instantiate(Resources.Load("voxel")) as GameObject;
		prototype.transform.parent=terrain.transform;
		prototype.GetComponent<Voxel>().vControlor=this;
		return prototype;
	}
	
	// Update is called once per frame
	void Update ()
	{
		voxelCount=GetComponentsInChildren<Voxel>().Length;
	}

	public JSONNode GetState(){
		JSONClass stateJ=new JSONClass();
		JSONArray voxelsJ=new JSONArray();
		voxels=GetComponentsInChildren<Voxel>();
		foreach(Voxel v in voxels){
			if(!v.destoryFlag)
			voxelsJ.Add(FromVoxel(v));
		}
		stateJ.Add("voxels",voxelsJ);
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
		voxelPrototype=CreateVoxelPrototype();
		voxel=voxelPrototype.GetComponent<Voxel>().CloneMe(state.spriteRect,state.position,transform,state.divisionCount);
		bo=voxel.GetComponent<BreakableObject>();
		voxel.maxDivision=state.divisionMax;
		bo.durableValue=state.durableValue;
		voxel.transform.localScale=state.scale;
		GameObject.Destroy(voxelPrototype);
		return voxel;
	}


	public void SetState(JSONNode state){
		voxels=GetComponentsInChildren<Voxel>();
		foreach(Voxel v in voxels){
			GameObject.Destroy(v.gameObject);
		}
		//GameObject.Destroy(voxelPrototype);
		List<Voxel> vs=new List<Voxel>();
		JSONArray arr=state["voxels"] as JSONArray;
		foreach(JSONNode node in arr){
			vs.Add(CreateVoxel(node as JSONClass));
		}
		voxels=vs.ToArray();
	}

}
