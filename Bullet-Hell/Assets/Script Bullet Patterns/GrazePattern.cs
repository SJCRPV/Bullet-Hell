﻿using UnityEngine;
using System.Collections;

public class GrazePattern : MonoBehaviour {

    public GameObject bulletPrefab;
    public float cooldownTimer;
	public float flipCooldownTimer;
    public float innerCooldownTimer;

    private float cooldownTimerStore;
    private GameObject bulletInstance;
    private Quaternion bulletRotation;
    private Vector3 correction;
    private float innerCooldownTimerStore;
	private float flipCooldownTimerStore;
	private bool? flipFlag;
	private bool startPattern;

    void Fire()
    {
		//Debug.Log("Fire!");
        bulletInstance = (GameObject)Instantiate(bulletPrefab, correction, new Quaternion(0, 0, 180, 0));
		bulletInstance.gameObject.layer = 11;
    }

    void FirePattern()
    {
		if(flipFlag == false)
		{
	        if(correction.x <= transform.position.x + 0.7f)
	        {
	            innerCooldownTimer -= Time.deltaTime;
				//Debug.Log(innerCooldownTimer);
	            if (innerCooldownTimer <= 0)
	            {
					Fire();
	                correction.x += 0.2f;
					innerCooldownTimer = innerCooldownTimerStore;
	            }
	        }
	        else
			{
				correction = transform.position;
				correction.x = transform.position.x + 0.5f;
				correction.y = transform.position.y - 0.5f;
				flipFlag = true;
			}
		}
		else if(flipFlag == true)
		{
			flipCooldownTimer -= Time.deltaTime;
			if(flipCooldownTimer <= 0)
			{
				if(correction.x >= transform.position.x - 0.7f)
				{
					innerCooldownTimer -= Time.deltaTime;
					//Debug.Log(innerCooldownTimer);
					if (innerCooldownTimer <= 0)
					{
						Fire();
						correction.x -= 0.2f;
						innerCooldownTimer = innerCooldownTimerStore;
					}
				}
				else
				{
					flipFlag = null;
				}
			}
		}
		else
		{
			flipCooldownTimer = flipCooldownTimerStore;
			cooldownTimer = cooldownTimerStore;
			flipFlag = false;
			startPattern = false;
			correction = transform.position;
			correction.x = transform.position.x - 0.5f;
			correction.y = transform.position.y - 0.5f;
		}
    }

	// Use this for initialization
	void Start () {
		flipFlag = false;
		startPattern = false;
        cooldownTimerStore = cooldownTimer;
        innerCooldownTimerStore = innerCooldownTimer;
		flipCooldownTimerStore = flipCooldownTimer;
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
			if(startPattern == false)
			{
				correction = transform.position;
				correction.x = transform.position.x - 0.5f;
				correction.y = transform.position.y - 0.5f;
				startPattern = true;
			}
            FirePattern();
        }
	}
}
