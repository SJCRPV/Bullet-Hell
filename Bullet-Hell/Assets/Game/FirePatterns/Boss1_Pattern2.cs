using UnityEngine;
using System.Collections;
using System;

public class Boss1_Pattern2 : MonoBehaviour, IFire
{

    public GameObject bulletPrefab;
    public float cooldownTimer;
    public float betweenBulletSpawnTimer;
    public int angleIncrement;

    private GameObject bulletInstance;
    private Movement_Generic genericMovementScript;
    private Movement_Boss bossMovementScript;
    private Quaternion bulletRotation;
    private float cooldownTimerStore;
    private float currentAngle;
    private float betweenBulletSpawnTimerStore;

    private void fire(float angle)
    {
        bulletRotation = Quaternion.identity;
        bulletRotation.eulerAngles = new Vector3(0, 0, angle);
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
        bulletInstance.gameObject.layer = 11;
        bulletInstance.transform.parent = this.transform;
        bulletInstance.transform.parent = gameObject.transform;
    }

    public void firePattern()
    {
        betweenBulletSpawnTimer -= Time.deltaTime;
        if (betweenBulletSpawnTimer <= 0 && bossMovementScript.getIsMoving() == false)
        {
            fire(currentAngle);
            fire(-currentAngle);
            currentAngle += angleIncrement;
            betweenBulletSpawnTimer = betweenBulletSpawnTimerStore;
        }

        if (currentAngle >= 540)
        {
            currentAngle = 180;
            cooldownTimer = cooldownTimerStore;
        }
    }

    public void assignMovement()
    {
        genericMovementScript = gameObject.GetComponentInParent<Movement_Generic>();
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
    }

    //Use this for initialization
    void Start()
    {
        cooldownTimerStore = cooldownTimer;
        currentAngle = 180;
        assignMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (genericMovementScript.getIsMoving() == false)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (bossMovementScript.getIsMoving())
        {
            cooldownTimer = cooldownTimerStore;
            betweenBulletSpawnTimer = betweenBulletSpawnTimerStore;
        }

        if (cooldownTimer <= 0)
        {
            firePattern();
        }
    }
}