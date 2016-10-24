using UnityEngine;
using System.Collections;
using System;

public class Boss1_Pattern2 : MonoBehaviour, IFire
{

    public GameObject bulletPrefab;
    public bool isFiring;
    public float betweenBulletSpawnTimer;
    public int angleIncrement;

    //private Movement_Generic genericMovementScript;
    private Movement_Boss bossMovementScript;
    private GameObject bulletInstance;
    private Quaternion bulletRotation;
    private float cooldownTimerStore;
    [SerializeField]
    private float currentAngle;
    [SerializeField]
    private float maxAngle;
    private float betweenBulletSpawnTimerStore;

    private void fire(float angle)
    {
        bulletRotation = Quaternion.identity;
        bulletRotation.eulerAngles = new Vector3(0, 0, angle);
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
        bulletInstance.gameObject.layer = 11;
        bulletInstance.transform.parent = gameObject.transform;
    }

    public void firePattern()
    {
        betweenBulletSpawnTimer -= Time.deltaTime;
        if (betweenBulletSpawnTimer <= 0)
        {
            fire(currentAngle);
            fire(-currentAngle);
            currentAngle += angleIncrement;
            betweenBulletSpawnTimer = betweenBulletSpawnTimerStore;
        }
    }

    public void assignMovement()
    {
        //genericMovementScript = gameObject.GetComponentInParent<Movement_Generic>();
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
    }

    //Use this for initialization
    void Start()
    {
        currentAngle = 180;
        assignMovement();
    }

    // Update is called once per frame
    void Update()
    {
        //Note: You probably want something that stops you from going beyond the 720 angle cycle regardless of whether the Boss still has time left before moving to the next node trio. You already have the ability of retrieving the amount of time left in said trio from the bossMovementScript
        if (bossMovementScript.getIsMoving())
        {
            isFiring = false;
            currentAngle = 180;
            betweenBulletSpawnTimerStore = betweenBulletSpawnTimer;
        }
        else
        {
            isFiring = true;
        }
    }
    void FixedUpdate()
    {
        if (isFiring && currentAngle < maxAngle)
        {
            firePattern();
        }
    }
}