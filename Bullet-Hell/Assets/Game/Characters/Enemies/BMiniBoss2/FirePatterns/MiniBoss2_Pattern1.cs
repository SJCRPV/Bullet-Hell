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

    private GameObject bulletInstance;
    private SlowHoming slowHomingScript;
    private TimedMoveForward timedMoveForwardScript;
    private Movement_Boss bossMovementScript;
    private Quaternion bulletRotation;
    private float cooldownTimerStore;
    private int rotationDegreeIncrement;
    private bool isFiringLeft;

    private void fire()
    {
        bulletInstance = (GameObject)Instantiate(missilePrefab, transform.parent.position, bulletRotation);
        slowHomingScript = bulletInstance.GetComponent<SlowHoming>();
        slowHomingScript.setRotationSpeed(missileRotationSpeed);
        bulletInstance.gameObject.layer = 11;
    }

    public void assignMovement()
    {
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
    }

    public void firePattern()
    {
        bulletRotation = Quaternion.identity;
        for (int i = 0; i < numMissiles; i += rotationDegreeIncrement)
        {
            if (isFiringLeft)
            {
                bulletRotation.eulerAngles = new Vector3(0, 0, -(i * rotationDegreeIncrement));
            }
            else
            {
                bulletRotation.eulerAngles = new Vector3(0, 0, (i * rotationDegreeIncrement));
            }
            isFiringLeft = !isFiringLeft;
            Invoke("fire", betweenBulletSpawnTimer * i);
        }
    }

    // Use this for initialization
    void Start () {
        cooldownTimerStore = cooldownTimer;
        bossMovementScript = GetComponent<Movement_Boss>();
        isFiringLeft = true;
        rotationDegreeIncrement = maxRotationDegrees / numMissiles;
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;

	    if(cooldownTimer <= 0)
        {
            firePattern();
            cooldownTimer = cooldownTimerStore;
        }
	}
}
