using UnityEngine;
using System.Collections;
using System;

public class MiniBoss2_Pattern1 : MonoBehaviour, IFire {

    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private float cooldownTimer;
    [SerializeField]
    private float betweenBulletSpawnTimer;
    [SerializeField]
    private int startingRotationDegrees;
    [SerializeField]
    private int maxRotationDegrees;
    [SerializeField]
    private int numMissiles;
    [SerializeField]
    private int missileRotationSpeed;
    [SerializeField]
    private int rotationDegreeIncrement;
    private GameObject bulletInstance;
    private SlowHoming slowHomingScript;
    private TimedMoveForward timedMoveForwardScript;
    private Movement_Generic genericMovementScript;
    private Movement_Boss bossMovementScript;
    private Quaternion bulletRotation;
    private float cooldownTimerStore;
    private float delayTimer;
    private bool isFiringLeft;

    private void fire()
    {
        bulletInstance = Instantiate(missilePrefab, transform.parent.position, bulletRotation);
        slowHomingScript = bulletInstance.GetComponent<SlowHoming>();
        slowHomingScript.setRotationSpeed(missileRotationSpeed);
        timedMoveForwardScript = bulletInstance.GetComponent<TimedMoveForward>();
        timedMoveForwardScript.setMoveTimer(delayTimer);
        bulletInstance.gameObject.layer = 11;
    }

    public void assignMovement()
    {
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
        genericMovementScript = gameObject.GetComponentInParent<Movement_Generic>();
    }

    public void firePattern()
    {
        bulletRotation = Quaternion.identity;
        for (int i = 0, tempDegrees = 0; i < numMissiles && tempDegrees < maxRotationDegrees; i++, tempDegrees += rotationDegreeIncrement)
        {
            if (isFiringLeft)
            {
                bulletRotation.eulerAngles = new Vector3(0, 0, -(tempDegrees));
            }
            else
            {
                bulletRotation.eulerAngles = new Vector3(0, 0, (tempDegrees));
            }
            //Debug.Log("bulletRotation.eulerAngles is " + bulletRotation.eulerAngles);
            delayTimer = i * betweenBulletSpawnTimer;
            isFiringLeft = !isFiringLeft;
            fire();
        }
        bossMovementScript.setIsMoving(false);
    }

    // Use this for initialization
    void Start () {
        assignMovement();
        cooldownTimerStore = cooldownTimer;
        isFiringLeft = true;
        rotationDegreeIncrement = maxRotationDegrees / numMissiles;
	}
	
	// Update is called once per frame
	void Update () {
        if (genericMovementScript.getIsMoving() == false && bossMovementScript.getIsMoving() == false)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = cooldownTimerStore;
        }

        if (cooldownTimer <= 0)
        {
            firePattern();
            cooldownTimer = cooldownTimerStore;
        }
	}
}
