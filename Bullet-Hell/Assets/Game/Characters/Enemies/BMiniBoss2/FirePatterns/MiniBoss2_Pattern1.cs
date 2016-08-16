using UnityEngine;
using System.Collections;
using System;

public class MiniBoss2_Pattern1 : MonoBehaviour, IFire {

    public GameObject missilePrefab;
    public float cooldownTimer;
    public float betweenBulletSpawnTimer;
    public int numOfMissiles;

    private GameObject bulletInstance;
    private Movement_Boss bossMovementScript;
    private Quaternion bulletRotation;
    private float cooldownTimerStore;
    //private float betweenBulletSpawnTimerStore;
    private bool isFiringLeft;

    private void fire()
    {
        bulletRotation = Quaternion.identity;
        if(isFiringLeft)
        {
            bulletRotation.eulerAngles = new Vector3(0, 0, -180);
        }
        else
        {
            bulletRotation.eulerAngles = new Vector3(0, 0, 180);
        }
        isFiringLeft = !isFiringLeft;
        bulletInstance = (GameObject)Instantiate(missilePrefab, transform.parent.position, bulletRotation);
        bulletInstance.gameObject.layer = 11;
    }

    public void assignMovement()
    {
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
    }

    public void firePattern()
    {
        for(int i = 0; i < numOfMissiles; i++)
        {
            Invoke("fire", betweenBulletSpawnTimer * i);
        }
    }

    // Use this for initialization
    void Start () {
        cooldownTimerStore = cooldownTimer;
        bossMovementScript = GetComponent<Movement_Boss>();
        isFiringLeft = true;
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
