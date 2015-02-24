using System;
using UnityEngine;
public class MyTouch
{
	int m_FingerId;
	Vector2 m_Position;
	TouchPhase m_Phase;
	public MyTouch (int id,Vector2 pos,TouchPhase p)
	{
		m_FingerId=id;
		m_Position=pos;
		m_Phase=p;
	}

	public MyTouch (Touch t)
	{
		m_FingerId=t.fingerId;
		m_Position=t.position;
		m_Phase=t.phase;
	}

	public int fingerId
	{
		get
		{
			return this.m_FingerId;
		}
	}

	public Vector2 position
	{
		get
		{
			return this.m_Position;
		}
	}

	public TouchPhase phase
	{
		get
		{
			return this.m_Phase;
		}
	}
}


