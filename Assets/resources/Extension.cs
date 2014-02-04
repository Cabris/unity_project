using UnityEngine;
using System;

public static class Extension
{

	public static Vector2 ToVector2(this Vector3 p){
		return new Vector2(p.x,p.y);
	}

}


