using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

    [SerializeField]
	private float bulletSpeed;
    [SerializeField]
	private float deathTimer;
	
    public float getBulletSpeed()
    {
        return bulletSpeed;
    }
    public void setSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		deathTimer -= Time.deltaTime;
		if(deathTimer <= 0)
		{
			Destroy(gameObject);
		}

		Vector3 newPos = transform.position;

		Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);

		newPos += transform.rotation * velocity;

		transform.position = newPos;
	}
}
