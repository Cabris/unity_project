using UnityEngine;
using System.Collections;

public class TerrainEditor : MonoBehaviour {

	public Terrain terrain;
	DrawBrush drawBrush;
	EraseBrush eraseBrush;
	public GameObject drawButton,eraseButton;

	// Use this for initialization
	void Start () {
		drawBrush=GetComponentInChildren<DrawBrush>();
		drawBrush.terrain=terrain;
		eraseBrush=GetComponentInChildren<EraseBrush>();
		UIEventListener.Get(drawButton).onClick += onButtonClick;
		UIEventListener.Get(eraseButton).onClick += onButtonClick;
	}
	
	// Update is called once per frame
	void Update () {
	


	}

	void onButtonClick(GameObject button){
		if(button.name=="Draw"){
			eraseBrush.gameObject.SetActive(false);
			drawBrush.gameObject.SetActive(true);
		}
		else if(button.name=="Erase"){
			eraseBrush.gameObject.SetActive(true);
			drawBrush.gameObject.SetActive(false);
		}


	}

}
