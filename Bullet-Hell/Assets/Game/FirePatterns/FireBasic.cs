using UnityEngine;
using System.Collections;
using System;

public class FireBasic : MonoBehaviour, IFire {

    public GameObject bulletPrefab;
    public float cooldownTimer;

    private GameObject bulletInstance;
    private float cooldownTimerStore;
    private Vector3 offset;

    void fire()
    {
        //Debug.Log("Dakka Dakka");
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - offset, new Quaternion(0, 0, 180, 0));
        cooldownTimer = cooldownTimerStore;
        bulletInstance.gameObject.layer = 11;
    }

    public void firePattern()
    {
        fire();
    }

	// Use this for initialization
	void Start () {
        offset = new Vector2(0, 0.5f);
        cooldownTimerStore = cooldownTimer;
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer <= 0)
        {
            fire();
            cooldownTimer = cooldownTimerStore;
        }
	}
}
