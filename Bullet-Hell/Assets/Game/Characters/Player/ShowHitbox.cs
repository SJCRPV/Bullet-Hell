using UnityEngine;
using System.Collections;

public class ShowHitbox : MonoBehaviour {

	SpriteRenderer spriteRend;

	// Use this for initialization
	void Start () {
		spriteRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Show Hitbox") != 0)
		{
			if(spriteRend != null)
			{
				spriteRend.enabled = true;
			}
		}
		else
		{
			spriteRend.enabled = false;
		}
	}
}
