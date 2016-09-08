using UnityEngine;
using System.Collections;
using System;

public class MiniBoss2_Pattern1 : MonoBehaviour, IFire {

    //CLEANING: Privatize any unnecessary public variables and make the necessary getters and setters to compliment it
    public GameObject missilePrefab;
    public float cooldownTimer;
    public float betweenBulletSpawnTimer;
    public int startingRotationDegrees;
    public int maxRotationDegrees;
    public int numMissiles;
    public int missileRotationSpeed;

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
        bulletInstance = (GameObject)Instantiate(missilePrefab, transform.parent.position, bulletRotation);
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
        //FIX: Bullets don't spawn at the right angles yet. Almost.
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
            Debug.Log("bulletRotation.eulerAngles is " + bulletRotation.eulerAngles);
            delayTimer = i * betweenBulletSpawnTimer;
            isFiringLeft = !isFiringLeft;
            fire();
        }
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

        if (cooldownTimer <= 0)
        {
            firePattern();
            cooldownTimer = cooldownTimerStore;
        }
	}
}
