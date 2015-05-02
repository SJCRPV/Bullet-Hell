using UnityEngine;
using System.Collections;

public class Boss1Ballistics : MonoBehaviour {

	public GameObject bulletPrefab;
	public float cooldownTimer;
	public int cooldownRoundLimiter;
	public float innerCooldownTimer;

	private float cooldownTimerStore;
	private Vector3 offset = new Vector3(0, 0.5f, 0);
	private float innerCooldownTimerStore;
	private GameObject bulletInstance;
	private Quaternion bulletRotation;

	void Fire()
	{
		for(; cooldownRoundLimiter > 0; innerCooldownTimer -= Time.deltaTime)
		{
			if(innerCooldownTimer <= 0)
			{
				int angleJump = -160;
				for(int i = 0; i <= 15; i++)
				{
					bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + angleJump, transform.rotation.w);
					bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - offset , bulletRotation);
				}
				innerCooldownTimer = innerCooldownTimerStore;
				cooldownRoundLimiter--;
			}
		}
	}

	// Use this for initialization
	void Start () {
		cooldownTimerStore = cooldownTimer;
		innerCooldownTimerStore = innerCooldownTimer;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

		if(cooldownTimer <= 0)
		{
			Fire();
		}
	}
}
