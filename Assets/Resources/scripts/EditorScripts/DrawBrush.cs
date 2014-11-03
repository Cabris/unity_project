using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawBrush : BaseBrush
{
	public Terrain terrain;
	Vector2 posOffset;
	[SerializeField]
	bool isOverlay;
	bool isMousePress;
	[SerializeField]
	List<BreakableObject> bos;
	[SerializeField]
	GameObject voxelPrefab; 
	Vector3 newPos;
	
	protected override void Start ()
	{
		base.Start ();
		isOverlay = false;
		isMousePress = false;
		bos = new List<BreakableObject> ();
		posOffset = new Vector2 ();
	}
	
	protected override void Update ()
	{
		base.Update ();
		isOverlay = bos.Count > 0;
		if (isOverlay)
			spriteRenderer.color = Color.red;
		else
			spriteRenderer.color = Color.white;

	}
	
	void FixedUpdate ()
	{
		
	}
	
	protected override void mouseDown (int button)
	{
		isMousePress = true;
	}
	
	protected override void mouseUp (int button)
	{
		isMousePress = false;
		if(!isOverlay&&IsActive){
		Voxel prototype=voxelPrefab.GetComponent<Voxel>();
		Rect r=spriteRenderer.sprite.rect;
		Vector3 pos=transform.position;
		Voxel v=prototype.CloneMe(r,pos,terrain.transform,Extension.toInt(div));
		v.transform.position=newPos;
			Debug.Log(v.transform.position+", "+newPos);
		}
	}
	
	protected override void updatePosition ()
	{
		int td = breakController.maxDivision;
		Vector2 divGrid = new Vector2 (Mathf.Pow (2, td), Mathf.Pow (2, td));
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = mousePosInWorld;
		newPos = culacPos (transform, originRect, divGrid, new Vector2 ());
		transform.position = newPos;
	}
	
	Vector3 culacPos (Transform baseT, Rect baseR, Vector2 gridSize, Vector2 index)
	{
		float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
		Vector3 _p = baseT.position;
		Vector2 unitSize = new Vector2 (baseR.width * baseT.localScale.x / gridSize.x, 
		                                baseR.height * baseT.localScale.y / gridSize.y) / pixelsToUnits;
		Vector3 p = _p;
		Vector2 up = new Vector2 ();
		up.x = Extension.toInt (_p.x / unitSize.x);
		up.y = Extension.toInt (_p.y / unitSize.y);
		p = new Vector3 (up.x * unitSize.x, up.y * unitSize.y);
		if (div == breakController.maxDivision) {
			p.x += posOffset.x;
			p.y -= posOffset.y;
		}
		return p;
	}
	
	protected override void updateBrushSize (float d)
	{
		base.updateBrushSize (d);
		BoxCollider collider = GetComponent<BoxCollider> ();
		Bounds b= spriteRenderer.bounds;
		Extension.ResetColliderSizeBySprite (b,collider);
		Vector2 divGrid = new Vector2 (Mathf.Pow (2, d), Mathf.Pow (2, d));
		float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
		Vector2 unitSize = new Vector2 (originRect.width * transform.localScale.x / divGrid.x, 
		                                originRect.height * transform.localScale.y / divGrid.y) / pixelsToUnits;
		posOffset = unitSize * .5f;
	}
	
	void  OnTriggerEnter (Collider other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null && !bos.Contains (bo)) {
			bos.Add (bo);
			//Debug.Log("OnTriggerEnter: "+bo.name);
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		BreakableObject bo = other.gameObject.GetComponent<BreakableObject> ();
		if (bo != null) {
			bos.Remove (bo);
			//Debug.Log("OnTriggerExit: "+bo.name);
		}
	}
	
	
	
}
