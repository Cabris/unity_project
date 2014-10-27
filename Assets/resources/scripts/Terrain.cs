using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour {

	Voxel[] voxels;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void updateVoxels(){
		voxels = GetComponentsInChildren<Voxel> ();
	}

	public void AddVoxel(Voxel v){}

}
