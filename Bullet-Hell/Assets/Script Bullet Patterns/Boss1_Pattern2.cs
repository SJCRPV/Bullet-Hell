using UnityEngine;
using System.Collections;

public class Boss1_Pattern2 : MonoBehaviour {

	public GameObject bulletPrefab;
	public float angleDispersion;
	public float cooldownTimer;
	public float betweenBulletSpawnTimer;

	private GameObject bulletInstance;
	private float angleDispersionStore;
	private float cooldownTimerStore;
	private float betweenBulletSpawnTimerStore;
	private Quaternion bulletRotation;
	private bool startCooldown;

	void Fire()
	{
		bulletRotation = Quaternion.identity;
		bulletRotation.eulerAngles = new Vector3(0, 0, angleDispersion);
		bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
		bulletInstance.gameObject.layer = 11;
	}

	public void FirePattern()
	{
		if(angleDispersion <= 720)
		{
			betweenBulletSpawnTimer -= Time.deltaTime;
			if(betweenBulletSpawnTimer <= 0)
			{
				angleDispersion += angleDispersionStore;
				Fire ();
				betweenBulletSpawnTimer = betweenBulletSpawnTimerStore;
			}
		}
		if(angleDispersion > 720)
		{
			cooldownTimer = cooldownTimerStore;
			startCooldown = true;
		}
	}

	// Use this for initialization
	void Start () {
		angleDispersionStore = angleDispersion;
		cooldownTimerStore = cooldownTimer;
		betweenBulletSpawnTimerStore = betweenBulletSpawnTimer;
		angleDispersion = 720;
		startCooldown = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(startCooldown)
		{
			cooldownTimer -= Time.deltaTime;
			angleDispersion = 0;
		}

		if(cooldownTimer <= 0)
		{
			startCooldown = false;
			FirePattern();
		}
	}
}
