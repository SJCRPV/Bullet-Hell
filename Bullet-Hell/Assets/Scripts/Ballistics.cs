using UnityEngine;
using System.Collections;

public class Ballistics : MonoBehaviour {

	public GameObject bulletPrefab;
	public float cooldownTimer = 0.25f;
	//public float timeUntilFire =
	float realCooldownTimer;
	Vector3 offset = new Vector3(0, 0.5f, 0);
	//static EnemySpawn enemyFireScript;

	// Use this for initialization
	void Start () {
		realCooldownTimer = cooldownTimer;
		//enemyFireScript = gameObject.GetComponent<EnemySpawn>();
		//Debug.Log (enemyFireScript);
	}
	
	// Update is called once per frame
	void Update () {
		Fire();
	}

	public void Fire()
	{
		//Debug.Log("Dakka Dakka");
		realCooldownTimer -= Time.deltaTime;
		
		if( realCooldownTimer <= 0)
		{
			if(gameObject.layer == 8)
			{
				Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
				realCooldownTimer = cooldownTimer;
			}
			else if(gameObject.layer == 9)
			{
				Instantiate(bulletPrefab, transform.position - offset, Quaternion.identity);
				realCooldownTimer = cooldownTimer;
			}
		}
	}
}
