using UnityEngine;
using System.Collections;

public class BlockInteractionChild : MonoBehaviour {

	BlockInteraction blockInteractionScript;

	public int speed;

	void OnCollisionStay2D(Collision2D collider)
	{
		if(collider.gameObject.layer == 12)
		{
			collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, speed*Time.deltaTime);
		}
	}

	// Use this for initialization
	void Start () {
		blockInteractionScript = GetComponentInParent<BlockInteraction>();
	}
}
