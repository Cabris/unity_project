
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Voxel : MonoBehaviour
{
	public BreakableObjectController vControlor;
	public int b_f = 0;//0~maxB
	public	int maxB = 4;
	public bool breakFlag = false;
	Vector2 iniSize;
	
	// Use this for initialization
	void Start ()
	{	BoxCollider2D collider2D = GetComponent<BoxCollider2D> ();
		iniSize=collider2D.size;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public Voxel[] Break (int gridX, int gridY)
	{
		List<Voxel> voxels = new List<Voxel> ();
		if (breakFlag)
			return voxels.ToArray ();
		if (b_f >= maxB) {
			DestoryMe ();
			return new Voxel[0];
		}
		
		if (gridX > 1 && gridY > 1 && b_f < maxB) {
			Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
			Rect parentRect = _sprite.rect;
			BoxCollider2D collider2D = GetComponent<BoxCollider2D> ();
			Vector2 gridSize = new Vector2 (gridX, gridY);
			for (int i=0; i<gridY; i++) {
				for (int j=0; j<gridX; j++) {
					Rect rect = culacRect (parentRect, gridSize, new Vector2 (j, i));
					Vector3 pos = culacPos (this.transform, parentRect, gridSize, new Vector2 (j, i));
					Voxel voxel=CloneMe(rect,pos,transform.parent,this.b_f + 1);
					voxels.Add (voxel);
				}
			}
			breakFlag = true;
			DestoryMe ();
		} else
			voxels.Add (this);
		return voxels.ToArray ();
	}


	public Voxel CloneMe (Rect rect,Vector3 pos,Transform parent,int b_f)
	{
		Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
		Texture2D tex2D = _sprite.texture;
		Vector2 pivot = new Vector2 (0.5f, 0.5f);
		Sprite sprite  = Sprite.Create (tex2D, rect, pivot);
		Voxel voxel=CloneMe(sprite);
		Transform t = voxel.transform;
		t.position = pos;
		BoxCollider2D collider2D = voxel.GetComponent<BoxCollider2D> ();

		t.parent = parent;
		voxel.b_f =b_f;
		voxel.ResetColliderSizeBySprite();
		return voxel;
	}
	
	Voxel CloneMe (Sprite s)
	{
		GameObject go = GameObject.Instantiate (gameObject) as GameObject;
		Voxel vox = go.GetComponent<Voxel> ();
		vox.GetComponent<SpriteRenderer> ().sprite = s;
		return vox;
	}
	
	Rect culacRect (Rect baseR, Vector2 gridSize, Vector2 index)
	{
		Rect r = new Rect ();
		Vector2 topLeft, size;
		size = new Vector2 (baseR.width / gridSize.x, baseR.height / gridSize.y);
		topLeft = new Vector2 ();
		topLeft.x = baseR.xMin + size.x * index.x;
		topLeft.y = baseR.yMin + size.y * index.y;
		r.xMin = topLeft.x;
		r.yMin = topLeft.y;
		r.width = size.x;
		r.height = size.y;
		return r;
	}
	
	Vector3 culacPos (Transform baseT, Rect baseR, Vector2 gridSize, Vector2 index)
	{
		float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
		Vector3 p = new Vector3 ();
		Vector3 topLeft = baseT.position + (new Vector3 (-baseR.width*baseT.localScale.x / 4f, -baseR.height*baseT.localScale.y / 4f, 0) / pixelsToUnits);
		Vector2 unitSize = new Vector2 (baseR.width*baseT.localScale.x / gridSize.x, baseR.height*baseT.localScale.y / gridSize.y) / pixelsToUnits;
		p = topLeft + new Vector3 (unitSize.x * index.x, unitSize.y * index.y, 0);
		return p;
	}
	
	public void ResetColliderSizeBySprite(){
		BoxCollider2D collider2D = GetComponent<BoxCollider2D> ();
		SpriteRenderer sp=GetComponent<SpriteRenderer>();
		Bounds b=sp.sprite.bounds;
		Vector3 bs=b.size;
		Vector2 ls=new Vector2(b.size.x,b.size.y);
		collider2D.size=ls;
	}

	public void DestoryMe ()
	{
		GameObject.Destroy (gameObject);
	}

	
}

