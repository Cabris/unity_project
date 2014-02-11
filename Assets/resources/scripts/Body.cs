using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour
{
	
	[SerializeField]
	Hand[] hands;
	[SerializeField]
	bool[] links;
	int count = 0;
	[SerializeField]	
	float spring;
	[SerializeField]
	float damper;
//	[SerializeField]
//	float damper;
//	[SerializeField]
//	float damper;
//	[SerializeField]
//	float damper;
	
	// Use this for initialization
	void Start ()
	{
		links = new bool[hands.Length];
		for (int i=0; i<links.Length; i++)
			links [i] = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i=0; i<hands.Length; i++) {
			Hand h = hands [i];
			links [i] = h.IsLink;
			h.SetSpring (this.spring);
			h.SetDamper (this.damper);
		}
	}
	
	public void HandGoTo (Vector3 pos)
	{
		for (int i=0; i<links.Length; i++) {
			count++;
			count = count % hands.Length;
			Hand hand = hands [count];
			if (hand.IsLink) {
				hand.LockHand (pos);
				break;
			}
		}
	}
	
	public void ReleaseHandLock(){
		for (int i=0; i<hands.Length; i++) {
			Hand h = hands [i];
			releaseHandLock(h);
		}
	}
	
	void releaseHandLock(Hand h){
		h.FreeHand();
	}
	
	
}
