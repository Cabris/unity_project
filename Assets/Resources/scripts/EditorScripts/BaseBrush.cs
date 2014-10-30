using UnityEngine;
using System.Collections;

public class BaseBrush : MonoBehaviour {
	
	[SerializeField]
	protected float div;
	float _div;
	[SerializeField]
	protected BreakableObjectController breakController;
	protected SpriteRenderer spriteRenderer;
	protected Rect originRect;

	Vector3 max=new Vector3(1,1,1);
	Vector3 min=new Vector3(0.05f,0.05f,0.05f);

	// Use this for initialization
	protected virtual void Start () {
		float minF=Mathf.Pow(.5f,breakController.maxDivision);
		min=new Vector3(minF,minF,minF);
		spriteRenderer=GetComponent<SpriteRenderer> ();
		Sprite _sprite = spriteRenderer.sprite;
		originRect=_sprite.rect;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
		if (Input.GetMouseButtonDown(0))
			mouseDown(0);
		if(Input.GetMouseButtonUp(0))
			mouseUp(0);
		
		float wheelValue=Input.GetAxis("Mouse ScrollWheel");
		SetBrushSize(wheelValue);
		updatePosition ();	
	}
	
	protected virtual void mouseDown(int button){
		IsActive=true;
	}
	
	protected virtual void mouseUp(int button){
		IsActive=false;
	}
	
	protected virtual void updatePosition ()
	{
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosInWorld.z = 0;
		transform.position = mousePosInWorld;
	}
	
	void SetBrushSize(float sizeMulp){
		if (sizeMulp == 0)
						return;
		if(sizeMulp<0&&!(div<=0)){
			div--;
		}
		if(sizeMulp>0&&!(div>=breakController.maxDivision)){
			div++;
		}
		if(div!=_div)
			updateBrushSize(div);
		_div=div;
	}
	
	protected virtual void updateBrushSize(float d){
		Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
		Vector2 divGrid=new Vector2(Mathf.Pow(2,d),Mathf.Pow(2,d));
		Rect rect = Voxel.culacRect(originRect,divGrid,new Vector2());
		Texture2D tex2D = spriteRenderer.sprite.texture;
		Vector2 pivot = new Vector2 (0.5f, 0.5f);
		Sprite sprite = Sprite.Create (tex2D, rect, pivot);
		spriteRenderer.sprite=sprite;
	}
	

	
	public bool IsActive{ 
		get;
		set;
	}
}
