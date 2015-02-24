using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainEditor : MonoBehaviour {
	
	public Terrain terrain;
	DrawBrush drawBrush;
	EraseBrush eraseBrush;
	public GameObject drawButton,eraseButton,stateLabel,newButton,buttonPlay;
	Dictionary<string,BaseBrush> brushes=new Dictionary<string, BaseBrush>();
	[SerializeField]
	GameObject playerPrefab;
	public bool isPlaying=false;
	GameObject player;
	string buttonName;
	
	
	// Use this for initialization
	void Start () {
		drawBrush=GetComponentInChildren<DrawBrush>();
		drawBrush.terrain=terrain;
		eraseBrush=GetComponentInChildren<EraseBrush>();
		UIEventListener.Get(drawButton).onClick += onButtonClick;
		UIEventListener.Get(eraseButton).onClick += onButtonClick;
		UIEventListener.Get(newButton).onClick += onButtonClick;
		UIEventListener.Get(buttonPlay).onClick += onButtonClick;
		brushes.Add("Draw",drawBrush);
		drawBrush.editor=this;
		brushes.Add("Erase",eraseBrush);
		eraseBrush.editor=this;
		onButtonClick(drawButton);
	}
	
	// Update is called once per frame
	void Update () {
		float axisX= Input.GetAxis("Horizontal");
		float	axisY=Input.GetAxis("Vertical");
		Camera.main.gameObject.transform.Translate(new Vector3(axisX,axisY,0)*Time.deltaTime*10);
		
		foreach(string k in brushes.Keys){
			brushes[k].UpdateBrushSize=(k==buttonName);
			brushes[k].UpdatePosition=(k==buttonName);
			brushes[k].IsActive=(k==buttonName&&!isPlaying);
			if(k!=buttonName)
				brushes[k].transform.Translate(new Vector3(999,999,0));
		}
		
	}
	
	void onButtonClick(GameObject button){
		stateLabel.GetComponent<UILabel> ().text = button.name;
		buttonName=button.name;
		if(button==newButton){
			
		}
		if(button==buttonPlay){
			isPlaying=!isPlaying;
			if(isPlaying){
				player= GameObject.Instantiate(playerPrefab) as GameObject;
				player.transform.position=Vector3.zero;
			}else{
				GameObject.Destroy(player);
			}
			
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
