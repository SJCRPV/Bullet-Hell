using UnityEngine;
using System.Collections;

public class Boss2_FirePattern2 : MonoBehaviour, IFire 
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float cooldownTimer;
    [SerializeField]
    private int averageBulletCount;
    [SerializeField]
    private int bulletCountVariation;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;

    private GameObject bulletInstance;
    private Movement_Generic genericMovementScript;
    private Movement_Boss bossMovementScript;
    private float cooldownTimerStore;

    public void blast(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, new Quaternion(0, 0, 180, 0));
            bulletInstance.setSpeed(Random.Range(minSpeed, maxSpeed));
            bulletInstance.setEulerAngle(Random.Range(180.0f, 260.0f));
        }
    }

    public void firePattern()
    {
        if(bossMovementScript.getCurrentNodeTrioInUse() % 4 == 0)
        {
            int finalBulletCount = Random.Range(averageBulletCount - bulletCountVaration, averageBulletCount + bulletCountVariation);
            blast(finalBulletCount);
        }
    }

    public void assignMovement()
    {
        genericMovementScript = gameObject.GetComponentInParent<Movement_Generic>();
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
        if (genericMovementScript == null)
        {
            Debug.LogError("genericMovementScript is empty. Did not get the component from parent");
        }
        if (bossMovementScript == null)
        {
            Debug.LogError("bossMovementScript is empty. Did not get the component from parent");
        }
    }

    // Use this for initialization
    void Start()
    {
        cooldownTimerStore = cooldownTimer;
        inBetweenTimerStore = inBetweenTimer;
        assignMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (!genericMovementScript.getIsMoving() && !bossMovementScript.getIsMoving())
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
