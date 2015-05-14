using UnityEngine;
using System.Collections;

public class ConePattern : MonoBehaviour {

	public GameObject bulletPrefab;
	public float cooldownTimerStore;
	//The smaller the number, the bigger the spacing
	public float angleDispersion;

	private float cooldownTimer;
	private Vector3 offset = new Vector3(0, 0.5f, 0);
	private GameObject bulletInstance;
	private Quaternion bulletRotation;

	void Fire()
	{
		for(int i = 0; i <= 6; i++)
		{
			//Redo this code. Boss1Ballistics is a good example.
			switch(i)
			{
			case 0:
				bulletRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z - angleDispersion * 3,transform.rotation.w);
				break;

			case 1:
				bulletRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z - angleDispersion * 2,transform.rotation.w);
				break;

			case 2:
				bulletRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z - angleDispersion,transform.rotation.w);
				break;

			case 3:
				bulletRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z + 180,transform.rotation.w);
				break;

			case 4:
				bulletRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z + angleDispersion,transform.rotation.w);
				break;

			case 5:
				bulletRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z + angleDispersion * 2,transform.rotation.w);
				break;

			case 6:
				bulletRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z + angleDispersion * 3,transform.rotation.w);
				break;

			default:
				Debug.LogError("You got a number you were not supposed to. It's: " + i);
				break;
			}
			
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - offset, bulletRotation);
			bulletInstance.gameObject.layer = 11;
		}

		cooldownTimer = cooldownTimerStore;
	}

	// Use this for initialization
	void Start () {
		cooldownTimer = cooldownTimerStore;
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
