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
		voxels = GetComponentsInChildren<Voxel> ();
		string filename="map.txt";
		string filepath=Application.dataPath+"/resources/"+filename;
		StreamReader str=new StreamReader(filepath);
		string content=str.ReadToEnd();
		str.Close();
		
		JSONNode jn=JSON.Parse(content);
		string v=jn["firstName"].Value;
		string v2=jn["lastName"].Value;
		
		JSONArray ja=jn["phoneNumber"].AsArray;
		
		foreach(JSONNode n in ja){
			Debug.Log("type:"+n["type"].Value);
			Debug.Log("number:"+n["number"].Value);
		}
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
