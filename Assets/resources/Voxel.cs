using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Voxel : MonoBehaviour
{
		public Texture2D tex2D;
		public VoxelController vControlor;
		public GameObject button;
		// Use this for initialization
		void Start ()
		{
				UIEventListener.Get (button).onClick = ButtonClick;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void ButtonClick (GameObject button)
		{
//			Debug.Log("GameObject " + button.name);
				Break ();
		}

		void OnMouseDown ()
		{
				Break ();
		}

		public void Break ()
		{
				List<Voxel> voxels = new List<Voxel> ();
				Sprite _sprite = this.GetComponent<SpriteRenderer> ().sprite;
				Rect parentRect = _sprite.rect;
				int gridX = 2, gridY = 2;
				Vector2 gridSize = new Vector2 (gridX, gridY);
				for (int i=0; i<gridY; i++)
						for (int j=0; j<gridX; j++) {
								//Texture2D tex2D = this.GetComponent<SpriteRenderer> ().material.mainTexture as Texture2D;
								Rect rect = culacRect (parentRect, gridSize, new Vector2 (j, i));
								Vector2 pivot = new Vector2 (0.5f, 0.5f);
								Sprite sprite = _sprite;
								sprite = Sprite.Create (tex2D, rect, pivot);
								Vector3 pos = culacPos (this.transform, parentRect, gridSize, new Vector2 (j, i));
								Voxel voxel = CloneMe (sprite);
								Transform t = voxel.transform;
								t.position = pos;
								BoxCollider2D collider2D = voxel.GetComponent<BoxCollider2D> ();
								Vector2 scale = collider2D.size;
								scale.x /= gridSize.x;
								scale.y /= gridSize.y;
								collider2D.size = scale;
								//t.localScale = scale;
								t.parent = transform.parent;
								voxels.Add (voxel);
						}
				DestoryMe ();
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
				Vector3 topLeft = baseT.position + (new Vector3 (-baseR.width / 4f, -baseR.height / 4f, 0) / pixelsToUnits);
				Vector2 unitSize = new Vector2 (baseR.width / gridSize.x, baseR.height / gridSize.y) / pixelsToUnits;
				p = topLeft + new Vector3 (unitSize.x * index.x, unitSize.y * index.y, 0);
				return p;
		}

		Voxel CloneMe (Sprite s)
		{
				GameObject go = GameObject.Instantiate (gameObject) as GameObject;
				Voxel vox = go.GetComponent<Voxel> ();
				vox.GetComponent<SpriteRenderer> ().sprite = s;
				return vox;
		}

		void DestoryMe ()
		{
				GameObject.Destroy (gameObject);
		}





































}
