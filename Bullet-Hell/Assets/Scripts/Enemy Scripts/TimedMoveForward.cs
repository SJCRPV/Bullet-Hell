using UnityEngine;
using System.Collections;

public class TimedMoveForward : MonoBehaviour {

	Boss1Movement boss1MovementScript;

	public float bulletSpeed;
	public float deathTimer;
	public float moveTimer;

	void Start()
	{
		boss1MovementScript = GetComponent<Boss1Movement>();
	}

	// Update is called once per frame
	void Update () {
		moveTimer -= Time.deltaTime;
		if(moveTimer <= 0)
		{
			deathTimer -= Time.deltaTime;

			Vector3 newPos = transform.position;
			
			Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);
			
			newPos += transform.rotation * velocity;
			
			transform.position = newPos;
		}
		else
		{
			//Have the bullets follow the their spawn point until they move
			transform.position = boss1MovementScript.transform.position;
		}

		if(deathTimer <= 0)
		{
			Destroy(gameObject);
		}
	}
}
