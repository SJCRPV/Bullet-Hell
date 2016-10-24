using UnityEngine;
using System.Collections;

public class ShowEnlargedCollider : MonoBehaviour {

	CircleCollider2D circleCollider;
	SpriteRenderer spriteRenderer;

    private void adjustColliderSizeTo(string objectName)
    {
        float spriteDiameter = GameObject.Find(objectName).GetComponent<SpriteRenderer>().sprite.bounds.size.x * GameObject.Find(objectName).transform.localScale.x;
        GameObject.Find("Player").GetComponent<CircleCollider2D>().radius = spriteDiameter / 2;
    }

	// Use this for initialization
	void Start () {
		circleCollider = GetComponent<CircleCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Show Hitbox") != 0)
		{
			if(circleCollider != null)
			{
				circleCollider.enabled = true;
				spriteRenderer.enabled = true;
                adjustColliderSizeTo("PlayerHitbox");
			}
		}
		else
		{
			circleCollider.enabled = false;
			spriteRenderer.enabled = false;
            adjustColliderSizeTo("Player");
		}
	}
}
