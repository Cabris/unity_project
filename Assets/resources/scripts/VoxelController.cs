using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class VoxelController : MonoBehaviour
{

	[SerializeField]
	Voxel[] voxels;
	// Use this for initialization
	void Start ()
	{

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
		JSONClass goJ=new JSONClass();
		goJ.Add("b_f",new JSONData(v.b_f));
		goJ.Add("maxB",new JSONData(v.maxB));
		goJ.Add("breakFlag",new JSONData(v.breakFlag));
		goJ.Add("durableValue",new JSONData(b.durableValue));
		Transform t=v.transform;
		Vector3 pos=t.position;
		Vector3 scal=t.localScale;
		//Vector3 rot=t.localRotation;
		goJ.Add("pos",goJ.JsonVector3(pos));
		goJ.Add("scal",goJ.JsonVector3(scal));
		//goJ.Add("rot",goJ.JsonVector3(rot));

		return goJ;
	}


	public void SetState(JSONNode state){


	}

}
