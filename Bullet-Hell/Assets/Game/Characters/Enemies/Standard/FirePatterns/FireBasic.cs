using UnityEngine;
using System.Collections;
using System;

public class FireBasic : MonoBehaviour, IFire {

    public GameObject bulletPrefab;
    public float cooldownTimer;

    private Movement_Generic movement;
    private GameObject bulletInstance;
    private float cooldownTimerStore;
    private Vector3 offset;

    void fire()
    {
        //Debug.Log("Dakka Dakka");
        bulletInstance = Instantiate(bulletPrefab, transform.position - offset, new Quaternion(0, 0, 180, 0));
        cooldownTimer = cooldownTimerStore;
        bulletInstance.gameObject.layer = 11;
    }

    public void firePattern()
    {
        fire();
    }

    public void assignMovement()
    {
        movement = gameObject.GetComponent<Movement_Generic>();
    }

	// Use this for initialization
	void Start () {
        offset = new Vector2(0, 0.5f);
        cooldownTimerStore = cooldownTimer;
        assignMovement();
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer <= 0 && movement.getIsMoving() == false)
        {
            fire();
            cooldownTimer = cooldownTimerStore;
        }
	}
}
