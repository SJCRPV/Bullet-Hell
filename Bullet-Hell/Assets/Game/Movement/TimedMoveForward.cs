using UnityEngine;
using System.Collections;

public class TimedMoveForward : MonoBehaviour {

	//Boss1Movement boss1MovementScript;

	public float bulletSpeed;
	public float deathTimer;
	public float moveTimer;

    public void setBulletSpeed(float newBulletSpeed)
    {
        bulletSpeed = newBulletSpeed;
    }

    public void setDeathTimer(float newDeathTimer)
    {
        deathTimer = newDeathTimer;
    }

    public void setMoveTimer(float newMoveTimer)
    {
        moveTimer = newMoveTimer;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		moveTimer -= Time.deltaTime;
		if(moveTimer <= 0)
		{
			this.transform.parent = null;

			deathTimer -= Time.deltaTime;

			Vector3 newPos = transform.position;
			
			Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);
			
			newPos += transform.rotation * velocity;
			
			transform.position = newPos;
		}

		if(deathTimer <= 0)
		{
			Destroy(gameObject);
		}
	}
}
