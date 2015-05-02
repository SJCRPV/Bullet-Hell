using UnityEngine;
using System.Collections;

public class Ballistics : MonoBehaviour {

	public GameObject bulletPrefab;
	public float cooldownTimerStore;
	float cooldownTimer;
	Vector3 offset = new Vector3(0, 0.5f, 0);

	private GameObject bulletInstance;
	private Transform objectParent;

	// Use this for initialization
	void Start () {
		cooldownTimer = cooldownTimerStore;
		//objectParent = GameObject.Find(this.gameObject.name).transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		cooldownTimer -= Time.deltaTime;
		if( cooldownTimer <= 0)
		{
			Fire();
		}
	}

	public void Fire()
	{
		//Debug.Log("Dakka Dakka");
		//Player
		if(gameObject.layer == 8)
		{
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
			cooldownTimer = cooldownTimerStore;
			bulletInstance.gameObject.layer = 10;
		}

		//Enemy
		else if(gameObject.layer == 9)
		{
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - offset, new Quaternion(0,0,180,0));
			cooldownTimer = cooldownTimerStore;
			bulletInstance.gameObject.layer = 11;
		}
	}
}
