using UnityEngine;
using System;
using System.Collections.Generic;
using SimpleJSON;

public static class Extension
{
	//private static string TYPE_VECTOR3="Vector3";
	public static Vector2 ToVector2(this Vector3 p){
		return new Vector2(p.x,p.y);
	}

	public static RaycastHit2D[] CircleScan(Vector2 origin,float distance ,LayerMask layerMask ,Collider2D collider){
		int scanResolu=30;
		List<RaycastHit2D> hits=new List<RaycastHit2D>();
		for(int i=0;i<scanResolu;i++){
			float angle=((float)(i+1)/(float)scanResolu)*2*Mathf.PI;
			Vector2 direction=new Vector2(Mathf.Cos(angle),Mathf.Sin(angle)).normalized;
			RaycastHit2D hit=Physics2D.Raycast(origin,direction,distance,1<<layerMask.value);
			if(hit!=null&&hit.collider==collider){
				hits.Add(hit);
			}
		}
		return hits.ToArray();
	}

	public static JSONNode JsonVector3(this JSONNode n,Vector3 v){
		JSONData x=new JSONData(v.x);
		JSONData y=new JSONData(v.y);
		JSONData z=new JSONData(v.z);

		JSONClass vector3Json=new JSONClass();
		vector3Json.Add("x",x);
		vector3Json.Add("y",y);
		vector3Json.Add("z",z);
		//vector3Json.Value=TYPE_VECTOR3;
		return vector3Json;
	}

	public static JSONNode JsonRect(this JSONNode n,Rect r){
		JSONData x=new JSONData(r.x);
		JSONData y=new JSONData(r.y);
		JSONData width=new JSONData(r.width);
		JSONData height=new JSONData(r.height);
		
		JSONClass rectJson=new JSONClass();
		rectJson.Add("x",x);
		rectJson.Add("y",y);
		rectJson.Add("width",width);
		rectJson.Add("height",height);
		//vector3Json.Value=TYPE_VECTOR3;
		return rectJson;
	}

	public static Vector3 ToVector3(this JSONNode n){
		Vector3 v=new Vector3();
		v.x=n["x"].AsFloat;
		v.y=n["y"].AsFloat;
		v.z=n["z"].AsFloat;
		return v;
	}

	public static Rect ToRect(this JSONNode n){
		Rect r=new Rect();
		r.x=n["x"].AsFloat;
		r.y=n["y"].AsFloat;
		r.width=n["width"].AsFloat;
		r.height=n["height"].AsFloat;
		return r;
	}

}



















