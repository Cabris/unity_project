using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{

		public GameObject button;

		// Use this for initialization
		void Start ()
		{
				UIEventListener.Get (button).onClick += ButtonClick;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void ButtonClick (GameObject button)
		{
				Debug.Log ("GameObject " + button.name);

		}

}
