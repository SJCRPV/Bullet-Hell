using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	public float bulletSpeed = 1f;
	float deathTimer = 2.5f;
	
	// Update is called once per frame
	void Update () {
		deathTimer -= Time.deltaTime;
		if(deathTimer <= 0)
		{
			Destroy(gameObject);
		}

		Vector3 newPos = transform.position;
		if(gameObject.layer == 8)
		{
			newPos.y += bulletSpeed * Time.deltaTime;
		}
		else if(gameObject.layer == 9)
		{
			newPos.y -= bulletSpeed * Time.deltaTime;
		}

		transform.position = newPos;
	}
}
