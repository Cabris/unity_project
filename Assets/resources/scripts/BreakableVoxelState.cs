using System;
using SimpleJSON;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct BreakableVoxelState
{
	public int divisionCount{ get; set; }//0 1 2 3 4
	public	int divisionMax{ get; set; }
	//public bool breakFlag{ get; set; }
	public Vector3 position{ get; set; }
	public Vector3 scale{ get; set; }
	public float durableValue{ get; set; }
	public Rect spriteRect{ get; set; }
	
	public BreakableVoxelState (BreakableObject bo)
	{
		durableValue = bo.durableValue;
		Voxel v = bo.GetComponent<Voxel> ();
		divisionCount = v.divisionCount;
		divisionMax = v.maxDivision;
		//breakFlag = v.destoryFlag;
		position = bo.transform.position;
		scale = bo.transform.localScale;
		spriteRect = bo.GetComponent<SpriteRenderer> ().sprite.rect;
	}
	
	public BreakableVoxelState (JSONClass goJ)
	{
		divisionCount = goJ ["divisionCount"].AsInt;
		divisionMax = goJ ["divisionMax"].AsInt;
		//breakFlag = goJ ["breakFlag"].AsBool;
		durableValue = goJ ["durableValue"].AsFloat;
		position = goJ ["pos"].ToVector3 ();
		scale = goJ ["scal"].ToVector3 ();
		spriteRect=goJ["spriteRect"].ToRect();
	}
	
	public JSONClass ToJson ()
	{
		JSONClass goJ = new JSONClass ();
		goJ.Add ("divisionCount", new JSONData (divisionCount));
		goJ.Add ("divisionMax", new JSONData (divisionMax));
		//goJ.Add ("breakFlag", new JSONData (breakFlag));
		goJ.Add ("durableValue", new JSONData (durableValue));
		goJ.Add ("pos", goJ.JsonVector3 (position));
		goJ.Add ("scal", goJ.JsonVector3 (scale));
		goJ.Add("spriteRect",goJ.JsonRect(spriteRect));
		return goJ;
	}
	
}


