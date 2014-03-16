using UnityEngine;
using System;
using SimpleJSON;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;

public class StateSaveLoad : MonoBehaviour
{
	public GameObject buttonSave;
	public GameObject buttonLoad;
	public string filename = @"saves\save1.txt";
	BreakableObjectController vc;
	// Use this for initialization
	void Start ()
	{
		UIEventListener.Get (buttonSave).onClick += ButtonClick;
		UIEventListener.Get (buttonLoad).onClick += ButtonClick;
		vc = GetComponent<BreakableObjectController> ();
	}
	
	void ButtonClick (GameObject button)
	{
		//string filename=@"saves\save1.txt";
		if (button.tag == "save")
			Save ();
		if (button.tag == "load")
			Load ();
	}
	
	void Save ()
	{
		JSONNode root = vc.GetState ();
		root.SaveToFile(filename+".save");
		string stateStr = root.ToString ();
		Debug.Log (stateStr);
		System.IO.FileInfo fileIO=new System.IO.FileInfo (filename);
		string dPath=fileIO.Directory.FullName;
		System.IO.Directory.CreateDirectory (dPath);
		FileStream stream = File.Open(filename,FileMode.Create);
		StreamWriter writer = new System.IO.StreamWriter (stream);
		writer.Write (stateStr);
		writer.Close();
		
		
	}
	
	void Load ()
	{
		FileStream stream = File.Open(filename,FileMode.Open);
		
		StreamReader reader = new System.IO.StreamReader (stream);
		String jsonStr = reader.ReadToEnd ();
		JSONNode j = JSONClass.Parse (jsonStr);
		vc.SetState (j);
		reader.Close();
	}
	
	
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
