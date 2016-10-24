using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {

	GameObject temp;

	void OnTriggerEnter2D(Collider2D collider)
	{
		Destroy (gameObject);
	}

	void Update()
	{
        //CONSIDER: This is inefficient. Find a better way to make this check. Furthermore, see if it's even needed. There was some funkiness regarding the IgnoreCollision
		temp = GameObject.Find("WhenShiftPressedCollider");
		if(temp != null)
		{
			Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), temp.GetComponent<CircleCollider2D>(), true);
			Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), temp.GetComponent<CircleCollider2D>(), true);
		}
	}
}
