using UnityEngine;
using System.Collections;

public class TerrianEditor : MonoBehaviour {

	public Terrain terrain;
	DrawBrush drawBrush;


	// Use this for initialization
	void Start () {
		drawBrush=GetComponentInChildren<DrawBrush>();
		drawBrush.terrain=terrain;
	}
	
	// Update is called once per frame
	void Update () {
	


	}



}
