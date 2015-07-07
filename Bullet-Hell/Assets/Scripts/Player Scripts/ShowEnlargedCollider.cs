using UnityEngine;
using System.Collections;

public class ShowEnlargedCollider : MonoBehaviour {

	CircleCollider2D circleCollider;

	// Use this for initialization
	void Start () {
		circleCollider = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Show Hitbox") != 0)
		{
			if(circleCollider != null)
			{
				circleCollider.enabled = true;
			}
		}
		else
		{
			circleCollider.enabled = false;
		}
	}
}
