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
		temp = GameObject.Find("WhenShiftPressedCollider");
		if(temp != null)
		{
			Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), temp.GetComponent<CircleCollider2D>(), true);
		}
	}
}
