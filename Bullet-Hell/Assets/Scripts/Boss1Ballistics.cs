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
	
	void Fire()
	{
		float angleInDeg = 180;
		for(int i = 0; i < 21; i++)
		{
			if(i >= 7 && i < 10)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + angleDispersion * (i - 6), transform.rotation.w);
			}
			else if(i >= 11 && i < 14)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - angleDispersion * (i - 10), transform.rotation.w);
			}
			else if(i == 10)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + angleInDeg, transform.rotation.w);
			}
			else if(i < 7)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - (Mathf.Sin(angleDispersion * i) + angleInDeg * Mathf.Deg2Rad), transform.rotation.w);
			}
			else if(i >= 14)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + (Mathf.Sin(angleDispersion * (i - 14)) + angleInDeg * Mathf.Deg2Rad), transform.rotation.w);
			}

			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - offset , bulletRotation);
			bulletInstance.gameObject.layer = 11;
		}
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
