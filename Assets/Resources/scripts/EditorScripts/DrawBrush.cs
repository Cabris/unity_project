using UnityEngine;
using System.Collections;

public class DrawBrush : BaseBrush {
	public int div;
	public Terrain terrain;
	[SerializeField]
	BreakableObjectController breakController;
	SpriteRenderer spriteRenderer;
	Rect originRect;
	int _div;
	Vector2 posOffset;

	protected override void Start ()
	{
		base.Start ();
		spriteRenderer=GetComponent<SpriteRenderer> ();
		Sprite _sprite = spriteRenderer.sprite;
		originRect=_sprite.rect;
		posOffset=new Vector2();
	}

	protected override void Update ()
	{
		base.Update ();

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
		up.x=toInt(_p.x/unitSize.x);
		up.y=toInt(_p.y/unitSize.y);
		//up.x=(int)(up.x);
		//up.y=(int)(up.y);
		//
		Debug.Log("up: "+up+", u: "+unitSize.x);

		p = new Vector3(up.x*unitSize.x,up.y*unitSize.y);
		if(div==breakController.maxDivision){
			p.x+=posOffset.x;
			p.y-=posOffset.y;
		}

		return p;

	}

	int toInt(float f){
		if(f>0){
			return (int)(f+.5f);
		}
		if(f<0){
			return (int)(f-.5f);
		}
		return 0;
	}

	void updateBrushSize(){
		//Debug.Log(div);
		if(div!=_div)
		{
			Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
			Vector2 divGrid=new Vector2(Mathf.Pow(2,div),Mathf.Pow(2,div));
			Rect rect = Voxel.culacRect(originRect,divGrid,new Vector2());
			Texture2D tex2D = spriteRenderer.sprite.texture;
			Vector2 pivot = new Vector2 (0.5f, 0.5f);
			Sprite sprite = Sprite.Create (tex2D, rect, pivot);
			spriteRenderer.sprite=sprite;
			//GameObject.DestroyImmediate(_sprite,true);
			ResetColliderSizeBySprite ();

			float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
			Vector2 unitSize = new Vector2 (originRect.width * transform.localScale.x / divGrid.x, 
			                                originRect.height * transform.localScale.y / divGrid.y) / pixelsToUnits;
			posOffset=unitSize*.5f;

		}
		_div=div;
	}


	protected override void SetBrushSize (float sizeMulp)
	{
		if(sizeMulp<0&&!(div<=0)){
			div--;
		}
		if(sizeMulp>0&&!(div>=breakController.maxDivision)){
			div++;
		}
		updateBrushSize();
	}

	void ResetColliderSizeBySprite ()
	{
		BoxCollider2D collider2D = GetComponent<BoxCollider2D> ();
		Bounds b = spriteRenderer.sprite.bounds;
		Vector3 bs = b.size;
		Vector2 ls = new Vector2 (b.size.x, b.size.y);
		collider2D.size = ls;
	}

}
