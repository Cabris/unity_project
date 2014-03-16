
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Voxel : MonoBehaviour
{
	public BreakableObjectController vControlor;
	public int divisionCount = 0;//0~maxB
	[SerializeField]
	public	int maxDivision = 4;
	//[SerializeField]
	bool destoryFlag = false;
	[SerializeField]
	float lifeTime;
	[SerializeField]
	Vector2 unitSize;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		float dt = Time.deltaTime;
		if (destoryFlag)
			lifeTime -= dt;
		if (lifeTime <= 0) {
			GameObject.Destroy (gameObject);
		}
		unitSize = UnitSize;
	}
	
	public Voxel[] Break (int gridX, int gridY)
	{
		List<Voxel> voxels = new List<Voxel> ();
		if (divisionCount >= maxDivision) {
			Disintegration ();
			return voxels.ToArray ();
		}
		Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
		Rect parentRect = _sprite.rect;
		if (gridX > 1 && gridY > 1 && divisionCount < maxDivision) {
			
			BoxCollider2D collider2D = GetComponent<BoxCollider2D> ();
			Vector2 gridSize = new Vector2 (gridX, gridY);
			for (int i=0; i<gridY; i++) {
				for (int j=0; j<gridX; j++) {
					Rect rect = culacRect (parentRect, gridSize, new Vector2 (j, i));
					Vector3 pos = culacPos (this.transform, parentRect, gridSize, new Vector2 (j, i));
					Voxel voxel = CloneMe (rect, pos, transform.parent, this.divisionCount + 1);
					voxels.Add (voxel);
				}
			}
			GameObject.Destroy (gameObject);
			//Disintegration ();
		} else
			voxels.Add (this);
		return voxels.ToArray ();
	}
	
	public Voxel CloneMe (Rect rect, Vector3 pos, Transform parent, int dc)
	{
		Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
		Texture2D tex2D = _sprite.texture;
		Vector2 pivot = new Vector2 (0.5f, 0.5f);
		Sprite sprite = Sprite.Create (tex2D, rect, pivot);
		Voxel voxel = CloneMe (sprite);
		Transform t = voxel.transform;
		t.position = pos;
		BoxCollider2D collider2D = voxel.GetComponent<BoxCollider2D> ();
		
		t.parent = parent;
		voxel.divisionCount = dc;
		voxel.ResetColliderSizeBySprite ();
		return voxel;
	}
	
	private	Voxel CloneMe (Sprite s)
	{
		GameObject go = GameObject.Instantiate (gameObject) as GameObject;
		Voxel vox = go.GetComponent<Voxel> ();
		vox.GetComponent<SpriteRenderer> ().sprite = s;
		return vox;
	}
	
	private	Rect culacRect (Rect baseR, Vector2 gridSize, Vector2 index)
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
	
	private Vector3 culacPos (Transform baseT, Rect baseR, Vector2 gridSize, Vector2 index)
	{
		float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
		Vector3 p = new Vector3 ();
		Vector3 topLeft = baseT.position + (new Vector3 (-baseR.width * baseT.localScale.x / 4f, -baseR.height * baseT.localScale.y / 4f, 0) / pixelsToUnits);
		Vector2 unitSize = new Vector2 (baseR.width * baseT.localScale.x / gridSize.x, baseR.height * baseT.localScale.y / gridSize.y) / pixelsToUnits;
		p = topLeft + new Vector3 (unitSize.x * index.x, unitSize.y * index.y, 0);
		return p;
	}
	
	public void ResetColliderSizeBySprite ()
	{
		BoxCollider2D collider2D = GetComponent<BoxCollider2D> ();
		SpriteRenderer sp = GetComponent<SpriteRenderer> ();
		Bounds b = sp.sprite.bounds;
		Vector3 bs = b.size;
		Vector2 ls = new Vector2 (b.size.x, b.size.y);
		collider2D.size = ls;
	}
	
	public void Disintegration ()
	{
		destoryFlag = true;
		rigidbody2D.isKinematic = false;
		gameObject.layer = LayerMask.NameToLayer ("rubble");
		
		Vector2 f = new Vector2 ();
		//Random r=new Random();
		Random.seed = Time.frameCount;
		f.x = Random.Range (-5, 5);
		f.y = Random.Range (-1, 5);
		rigidbody2D.AddForce (f * 0.05f);
	}
	
	public Vector2 UnitSize {
		get {
			Vector2 size = new Vector2 ();
			float pixelsToUnits = CameraConfig.Singleten.PixelsToUnits;
			Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
			Rect rect = _sprite.rect;
			size.x = rect.width / pixelsToUnits;
			size.y = rect.height / pixelsToUnits;
			return size;
		}
	}
	
	public bool IsDestorying {
		get{ return destoryFlag;}
	}
	
}

