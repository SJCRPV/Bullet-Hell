using UnityEngine;
using System.Collections;
using System;

public class MiniBoss2_Pattern2 : MonoBehaviour, IFire {

    [SerializeField]
    private GameObject slicePrefab;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float cooldownTimer;

    private float cooldownTimerStore;
    private GameObject bulletInstance;
    private GameObject sliceInstance;
    private iTweenPath path;
    private Movement_Generic genericMovementScript;
    private Movement_Boss bossMovementScript;
    private Vector3 firstPosition;

    private void fireShotgun()
    {

    }

    private void fireSlice(bool inverted)
    {
        Vector3 temp = transform.position;
        temp.y -= 1;
        Quaternion tempRot = Quaternion.identity;
        if (inverted)
        {
            tempRot.eulerAngles = new Vector3(0, 0, 50);
        }
        else
        {
            tempRot.eulerAngles = new Vector3(0, 0, -50);
        }

        sliceInstance = (GameObject)Instantiate(slicePrefab, temp, tempRot);
        sliceInstance.transform.parent = gameObject.transform;
        GetComponent<ExplodeDownwards>().setInverted();
    }

    public void firePattern()
    {
        Debug.Log(bossMovementScript.getCurrentNodeTrioInUse());
        if(bossMovementScript.getCurrentNodeTrioInUse() == 2)
        {
            Debug.Log("FireFireFire!");
            Vector3 temp = transform.parent.position;
            fireSlice(false);
            Vector3.Lerp(temp, new Vector3(temp.x - 0.25f, temp.y), Time.deltaTime * 2);
            fireSlice(true);
            Vector3.Lerp(transform.parent.position, new Vector3(temp.x + 0.25f, temp.y), Time.deltaTime * 2);
            fireSlice(false);
            Vector3.Lerp(transform.parent.position, temp, Time.deltaTime * 2);
        }
        else
        {
            fireShotgun();
        }
    }

    public void assignMovement()
    {
        genericMovementScript = gameObject.GetComponentInParent<Movement_Generic>();
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
        if(genericMovementScript == null)
        {
            Debug.LogError("genericMovementScript is empty. Did not get the component from parent");
        }
        if(bossMovementScript == null)
        {
            Debug.LogError("bossMovementScript is empty. Did not get the component from parent");
        }
    }

    // Use this for initialization
    void Start () {
        path = gameObject.GetComponent<iTweenPath>();
        cooldownTimerStore = cooldownTimer;
        firstPosition = path.nodes[0];
        Debug.Log(firstPosition);
        assignMovement();
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
