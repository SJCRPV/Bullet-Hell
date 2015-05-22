using UnityEngine;
using System.Collections;

public class ConePattern : MonoBehaviour {

	public GameObject bulletPrefab;
	public float cooldownTimer;
	//The smaller the number, the bigger the spacing
	public float angleDispersion;

	private float cooldownTimerStore;
	private Vector3 offset = new Vector3(0, 0.5f, 0);
	private GameObject bulletInstance;
	private Quaternion bulletRotation;
	private float angleDispersionStore;

	void Fire()
	{
		for (angleDispersion = 150; angleDispersion <= 210; angleDispersion += angleDispersionStore * 2) 
		{
			bulletRotation = Quaternion.identity;
			bulletRotation.eulerAngles = new Vector3(0,0,angleDispersion);
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
			bulletInstance.gameObject.layer = 11;
		}
		angleDispersion = angleDispersionStore;

		cooldownTimer = cooldownTimerStore;
	}

	// Use this for initialization
	void Start () {
		cooldownTimerStore = cooldownTimer;
		angleDispersionStore = angleDispersion;
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
