using UnityEngine;
using SimpleJSON;
using System.Collections;

public class StateSaveLoad : MonoBehaviour {
	public GameObject buttonSave;
	public GameObject buttonLoad;
	public string filename=@"saves\save1.txt";
	VoxelController vc;
	// Use this for initialization
	void Start () {
		UIEventListener.Get (buttonSave).onClick += ButtonClick;
		UIEventListener.Get (buttonLoad).onClick += ButtonClick;
		vc=GetComponent<VoxelController>();
	}

	void ButtonClick(GameObject button){
		//string filename=@"saves\save1.txt";
		if(button.tag=="save")
			Save();
		if(button.tag=="load")
			Load();
	}
	
	void Save(){
		JSONNode root=vc.GetState();

		string stateStr=root.ToString();
		Debug.Log(stateStr);
		root.SaveToFile(filename);
	}
	
	void Load(){
		JSONNode j=JSONClass.LoadFromFile(filename);
		vc.SetState(j);

	}



	// Update is called once per frame
	void Update () {
	
	}
}
