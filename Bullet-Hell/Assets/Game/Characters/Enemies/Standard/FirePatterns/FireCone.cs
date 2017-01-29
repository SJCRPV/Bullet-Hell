using UnityEngine;
using System.Collections;
using System;

public class FireCone : MonoBehaviour, IFire {

	public GameObject bulletPrefab;
	public float cooldownTimer;
	//The smaller the number, the bigger the spacing
	public float angleDispersion;

    private Movement_Generic movement;
	private float cooldownTimerStore;
	private GameObject bulletInstance;
	private Quaternion bulletRotation;
	private float angleDispersionStore;

	void Fire()
	{
		for (angleDispersion = 150; angleDispersion <= 210; angleDispersion += angleDispersionStore * 2) 
		{
			bulletRotation = Quaternion.identity;
			bulletRotation.eulerAngles = new Vector3(0,0,angleDispersion);
			bulletInstance = Instantiate(bulletPrefab, transform.position, bulletRotation);
			bulletInstance.gameObject.layer = 11;
		}
		angleDispersion = angleDispersionStore;

		cooldownTimer = cooldownTimerStore;
	}

    public void firePattern()
    {
        Fire();
    }

    public void assignMovement()
    {
        movement = gameObject.GetComponent<Movement_Generic>();
    }

	// Use this for initialization
	void Start () {
		cooldownTimerStore = cooldownTimer;
		angleDispersionStore = angleDispersion;
        assignMovement();
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0 && movement.getIsMoving() == false)
		{
			firePattern();
		}
	}
}
