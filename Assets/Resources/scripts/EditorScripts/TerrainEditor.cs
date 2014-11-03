using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainEditor : MonoBehaviour {

	public Terrain terrain;
	DrawBrush drawBrush;
	EraseBrush eraseBrush;
	public GameObject drawButton,eraseButton,stateLabel;
	Dictionary<string,BaseBrush> brushes=new Dictionary<string, BaseBrush>();


	// Use this for initialization
	void Start () {
		drawBrush=GetComponentInChildren<DrawBrush>();
		drawBrush.terrain=terrain;
		eraseBrush=GetComponentInChildren<EraseBrush>();
		UIEventListener.Get(drawButton).onClick += onButtonClick;
		UIEventListener.Get(eraseButton).onClick += onButtonClick;
		brushes.Add("Draw",drawBrush);
		brushes.Add("Erase",eraseBrush);
		onButtonClick(drawButton);
	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void onButtonClick(GameObject button){
		foreach(string k in brushes.Keys){
			brushes[k].UpdateBrushSize=(k==button.name);
			brushes[k].UpdatePosition=(k==button.name);
			brushes[k].IsActive=(k==button.name);
			if(k!=button.name)
				brushes[k].transform.Translate(new Vector3(999,999,0));
			//brushes[k].gameObject.SetActive(k==button.name);
			//brushes[k].gameObject.SetActive(k==button.name);
		}
		stateLabel.GetComponent<UILabel> ().text = button.name;

	}

}
