﻿using UnityEngine;
using System.Collections;
using System;

public class MiniBoss1_Pattern1 : MonoBehaviour, IFire {
    //CLEANING: Update the variable declarations so that you don't have unnecessary public variables. Add get and set methods as well to compensate
    public GameObject bulletPrefab;
    public GameObject explodingBulletPrefab;
    public float startingDegrees;
    public float cooldownTimer;
    public float maxDegrees;
    public float degreeIncreasePerIteration;
    public float movingTimeBetweenBursts;
    public float stillTimeBetweenBursts;

    private Movement_Generic genericMovementScript;
    private Movement_Boss bossMovementScript;
    private float cooldownTimerStore;
    private float startingDegreesStore;
    private GameObject bulletInstance;
    private Quaternion bulletRotation;

    void FireMoving()
    {
        for (; startingDegrees <= maxDegrees; startingDegrees += degreeIncreasePerIteration)
        {
            bulletRotation = Quaternion.identity;
            bulletRotation.eulerAngles = new Vector3(0, 0, startingDegrees);
            bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
            bulletInstance.gameObject.layer = 11;
        }
        startingDegrees = startingDegreesStore;
        cooldownTimer = cooldownTimerStore;
    }
    void FireStill()
    {
        bulletInstance = (GameObject)Instantiate(explodingBulletPrefab, transform.position - Vector3.up/2 - Vector3.left/2, new Quaternion(0, 0, 180, 0));
        bulletInstance.gameObject.layer = 11;
        bulletInstance = (GameObject)Instantiate(explodingBulletPrefab, transform.position - Vector3.up/2 - Vector3.right/2, new Quaternion(0, 0, 180, 0));
        bulletInstance.gameObject.layer = 11;
        cooldownTimer = stillTimeBetweenBursts;
    }

    public void firePattern()
    {
        if(bossMovementScript.getIsMoving() == true && genericMovementScript.getIsMoving() == false)
        {
            FireMoving();
            Invoke("FireMoving", movingTimeBetweenBursts);
            Invoke("FireMoving", movingTimeBetweenBursts * 2);
        }
        else
        {
            FireStill();
        }
    }

    public void assignMovement()
    {
        genericMovementScript = gameObject.GetComponentInParent<Movement_Generic>();
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
    }

    // Use this for initialization
    void Start()
    {
        startingDegreesStore = startingDegrees;
        cooldownTimerStore = cooldownTimer;
        assignMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (genericMovementScript.getIsMoving() == false)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0)
        {
            firePattern();
        }
    }
}