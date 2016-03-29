using UnityEngine;
using System.Collections;
using System;

public class MiniBoss1_Pattern2 : MonoBehaviour, IFire {

    public GameObject bulletPrefab;
    public GameObject zigzagBulletPrefab;
    public float timeBetweenBursts;

    private GameObject bulletInstance;
    private GameObject zigzagBulletInstance;
    private Movement_Boss bossMovementScript;
    private bool spawnZigZag = false;
    private float timeBetweenBurstsStore;

    void fireLong()
    {
        //Debug.Log("Long");
        //Debug.Log("Coordinates: " + transform.position);
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - Vector3.up / 2, new Quaternion(0, 0, 180, 0));
        bulletInstance.gameObject.layer = 11;
        zigzagBulletInstance = (GameObject)Instantiate(zigzagBulletPrefab, transform.position - Vector3.up / 3 - Vector3.left / 3, new Quaternion(0, 0, 180, 0));
        zigzagBulletInstance.gameObject.layer = 11;
        zigzagBulletInstance = (GameObject)Instantiate(zigzagBulletPrefab, transform.position - Vector3.up / 3 - Vector3.right / 3, new Quaternion(0, 0, 180, 0));
        zigzagBulletInstance.gameObject.layer = 11;
    }

    void fireQuick()
    {
        //Debug.Log("Quick");
        //Debug.Log("Coordinates: " + transform.position);
        //Debug.Log(spawnZigZag);
        if(spawnZigZag)
        {
            zigzagBulletInstance = (GameObject)Instantiate(zigzagBulletPrefab, transform.position - Vector3.up / 2 - Vector3.left / 2, new Quaternion(0, 0, 180, 0));
            zigzagBulletInstance.gameObject.layer = 11;
            zigzagBulletInstance = (GameObject)Instantiate(zigzagBulletPrefab, transform.position - Vector3.up / 2 - Vector3.right / 2, new Quaternion(0, 0, 180, 0));
            zigzagBulletInstance.gameObject.layer = 11;
        }
        else
        {
            bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - Vector3.up / 2 - Vector3.left / 2, new Quaternion(0, 0, 180, 0));
            bulletInstance.gameObject.layer = 11;
            bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position - Vector3.up / 2 - Vector3.right / 2, new Quaternion(0, 0, 180, 0));
            bulletInstance.gameObject.layer = 11;
        }
        spawnZigZag = !spawnZigZag;
    }

    public void firePattern()
    {
        if(bossMovementScript.getCurrentNodePairInUse() == 1 || bossMovementScript.getCurrentNodePairInUse() == 3 || bossMovementScript.getCurrentNodePairInUse() == 5)
        {
            fireLong();
        }
        else if(bossMovementScript.getCurrentNodePairInUse() == 2 || bossMovementScript.getCurrentNodePairInUse() == 4 || bossMovementScript.getCurrentNodePairInUse() == 6)
        {
            fireQuick();
        }
        else
        {
            Debug.LogError("I don't know what to fire with the number " + bossMovementScript.getCurrentNodePairInUse());
        }
    }

    public void assignMovement()
    {
        bossMovementScript = GetComponentInParent<Movement_Boss>();
    }

    // Use this for initialization
    void Start () {
        assignMovement();
        timeBetweenBurstsStore = timeBetweenBursts;
	}
	
	// Update is called once per frame
	void Update () {
	    if(bossMovementScript.getIsMoving() == false)
        {
            timeBetweenBursts -= Time.deltaTime;
            if(timeBetweenBursts <= 0)
            {
                firePattern();
                timeBetweenBursts = timeBetweenBurstsStore;
            }
        }
        else
        {
            timeBetweenBursts = timeBetweenBurstsStore;
        }
	}
}
