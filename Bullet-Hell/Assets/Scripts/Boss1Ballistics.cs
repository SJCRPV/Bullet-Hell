using UnityEngine;
using System.Collections;

public class Boss1Ballistics : MonoBehaviour {

	public GameObject bulletPrefab;
	public float cooldownTimer;
	public int cooldownRoundLimiter;
	public float innerCooldownTimer;
	public float angleDispersion;

	private float cooldownTimerStore;
	private Vector3 offset = new Vector3(0, 0.5f, 0);
	private float innerCooldownTimerStore;
	private GameObject bulletInstance;
	private Quaternion bulletRotation;
	private int cooldownRoundLimiterStore;
	private float angleDispersionStore;
	
	void Fire()
	{
		for (; angleDispersion <= 10; angleDispersion += angleDispersionStore) 
		{
			bulletRotation = Quaternion.identity;
			bulletRotation.eulerAngles = new Vector3(0,0,angleDispersion);
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - offset, bulletRotation);
			bulletInstance.gameObject.layer = 11;
		}
		angleDispersion = angleDispersionStore;
	}

	void FirePattern()
	{
		innerCooldownTimer -= Time.deltaTime;
		if(innerCooldownTimer <= 0)
		{
			Fire();
			cooldownRoundLimiter--;
			innerCooldownTimer = innerCooldownTimerStore;
			if(cooldownRoundLimiter <= 0)
			{
				cooldownRoundLimiter = cooldownRoundLimiterStore;
				cooldownTimer = cooldownTimerStore;
			}
		}
	}

	// Use this for initialization
	void Start () {
		cooldownTimerStore = cooldownTimer;
		innerCooldownTimerStore = innerCooldownTimer;
		cooldownRoundLimiterStore = cooldownRoundLimiter;
		angleDispersionStore = angleDispersion;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
		if(cooldownTimer <= 0)
		{
			FirePattern();
		}
	}
}
