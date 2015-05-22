using UnityEngine;
using System.Collections;

public class Boss1_Pattern1 : MonoBehaviour {

	public GameObject bulletPrefab;
	public float angleDispersion;

	private float angleDispersionStore;
	private GameObject bulletInstance;
	private Quaternion bulletRotation;

	public void Fire()
	{
		for (angleDispersion = 111; angleDispersion <= 249; angleDispersion += angleDispersionStore) 
		{
			bulletRotation = Quaternion.identity;
			bulletRotation.eulerAngles = new Vector3(0,0,angleDispersion);
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
			bulletInstance.gameObject.layer = 11;
		}
		angleDispersion = angleDispersionStore;
	}

	// Use this for initialization
	void Start () {
		angleDispersionStore = angleDispersion;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
