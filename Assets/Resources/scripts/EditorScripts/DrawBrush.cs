using UnityEngine;
using System.Collections;

public class DrawBrush : BaseBrush {

	public Terrain terrain;
	Vector2 posOffset;

	protected override void Start ()
	{
		base.Start ();

		posOffset=new Vector2();
	}

	protected override void Update ()
	{
		base.Update ();
		if (IsActive) {
		
		}
	}

	protected override void mouseDown (int button)
	{
		base.mouseDown (button);
	}

	protected override void mouseUp (int button)
	{
		base.mouseUp (button);
	}

	protected override void updatePosition ()
	{
		int td=breakController.maxDivision;
		Vector2 divGrid=new Vector2(Mathf.Pow(2,td),Mathf.Pow(2,td));
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = mousePosInWorld;
		Vector3 newPos=culacPos(transform,originRect,divGrid,new Vector2());
		transform.position=newPos;
	}

	Vector3 culacPos (Transform baseT, Rect baseR, Vector2 gridSize, Vector2 index)
	{
		float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
		Vector3 _p=baseT.position;
		Vector2 unitSize = new Vector2 (baseR.width * baseT.localScale.x / gridSize.x, 
		                                baseR.height * baseT.localScale.y / gridSize.y) / pixelsToUnits;
		Vector3 p=_p;
		Vector2 up=new Vector2();
		up.x=Extension.toInt(_p.x/unitSize.x);
		up.y=Extension.toInt(_p.y/unitSize.y);
		p = new Vector3(up.x*unitSize.x,up.y*unitSize.y);
		if(div==breakController.maxDivision){
			p.x+=posOffset.x;
			p.y-=posOffset.y;
		}
		return p;
	}

	protected override void updateBrushSize(float d){
		base.updateBrushSize(d);
		BoxCollider2D collider2D = GetComponent<BoxCollider2D> ();
		ResetColliderSizeBySprite (collider2D);
		Vector2 divGrid=new Vector2(Mathf.Pow(2,d),Mathf.Pow(2,d));
		float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
		Vector2 unitSize = new Vector2 (originRect.width * transform.localScale.x / divGrid.x, 
			                                originRect.height * transform.localScale.y / divGrid.y) / pixelsToUnits;
		posOffset=unitSize*.5f;
	}



}
