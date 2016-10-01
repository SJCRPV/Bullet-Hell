using UnityEngine;
using System.Collections;

public class ShowEnlargedCollider : MonoBehaviour {

	CircleCollider2D circleCollider;
    CircleCollider2D parentCollider;
	SpriteRenderer spriteRenderer;

    private void adjustColliderSizeTo(string objectName)
    {
        Vector3 spriteSize = GameObject.Find(objectName).GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<CircleCollider2D>().size = spriteSize;
    }

	// Use this for initialization
	void Start () {
		circleCollider = GetComponent<CircleCollider2D>();
        parentCollider = GetComponentInParent<CircleCollider2D>();
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
			}
		}
		else
		{
			circleCollider.enabled = false;
			spriteRenderer.enabled = false;
		}
	}
}
