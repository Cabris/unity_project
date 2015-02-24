using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Terrain : MonoBehaviour {

	HashSet<Voxel> voxels;

	// Use this for initialization
	void Start () {
		voxels=new HashSet<Voxel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void updateVoxels(){
		//voxels = GetComponentsInChildren<Voxel> ();
	}

	public void AddVoxel(Voxel v){
		voxels.Add(v);
	}

	public void RemoveVoxel(Voxel v){
		voxels.Remove(v);
	}

}
