using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class BreakableObjectController : MonoBehaviour
{
	GameObject voxelPrototype;
	[SerializeField]
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
		prototype.transform.parent=this.transform;
		prototype.GetComponent<Voxel>().vControlor=this;
		return prototype;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public JSONNode GetState(){
		JSONClass stateJ=new JSONClass();
		JSONArray voxelsJ=new JSONArray();
		voxels=GetComponentsInChildren<Voxel>();
		foreach(Voxel v in voxels){
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
		GameObject voxelGo=CreateVoxelPrototype();
		voxel=voxelGo.GetComponent<Voxel>();
		bo=voxelGo.GetComponent<BreakableObject>();
		voxel.b_f=state.b_f;
		voxel.breakFlag=state.breakFlag;
		voxel.maxB=state.maxB;
		voxelGo.transform.position=state.position;
		voxelGo.transform.localScale=state.scale;
		bo.durableValue=state.durableValue;

		return voxel;
	}


	public void SetState(JSONNode state){
		JSONArray arr=state["voxels"] as JSONArray;
		foreach(JSONNode node in arr){
			//Debug.Log(node.ToString());
		}
	}

}
