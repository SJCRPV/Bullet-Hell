using UnityEngine;
using System.Collections;

public class PlayerBallistics : MonoBehaviour {

	public GameObject bulletPrefab;
	public float cooldownTimerStore;
	float cooldownTimer;
	Vector3 offset = new Vector3(0, 0.5f, 0);
	
	private GameObject bulletInstance;
	private Transform objectParent;

	void fire()
	{
		bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
		cooldownTimer = cooldownTimerStore;
		bulletInstance.gameObject.layer = 10;
	}

	// Use this for initialization
	void Start () {
		cooldownTimer = cooldownTimerStore;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime; 

		if(cooldownTimer <= 0 && Input.GetButton("Fire1"))
		{
			fire();
		}
	}
}
