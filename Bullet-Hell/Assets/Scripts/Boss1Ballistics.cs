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
		float degToRad = 180;
		Debug.Log("Fired!");
		for(int i = 0; i < 15; i++)
		{
			if(i == 7)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + degToRad * Mathf.Deg2Rad, transform.rotation.w);
			}
			else if(i < 7)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - (Mathf.Sin(angleDispersion * i) + degToRad * Mathf.Deg2Rad), transform.rotation.w);
			}
			else if(i > 7)
			{
				bulletRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + (Mathf.Sin(angleDispersion * (i - 7)) + degToRad * Mathf.Deg2Rad), transform.rotation.w);
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
