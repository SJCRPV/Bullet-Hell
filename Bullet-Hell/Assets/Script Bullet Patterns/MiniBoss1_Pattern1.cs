using UnityEngine;
using System.Collections;

public class MiniBoss1_Pattern1 : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject bulletPrefabExplode;
    [HideInInspector]
    public EnemyMovement enemyMovementScript;
    public float angleDispersion;
    public float timeUntilMove;
    public float cooldownMovingTimer;
    public float cooldownStillTimer;
    public float innerCooldownMovingTimer;
    public float innerCooldownStillTimer;
    public int roundsBeforeCooldownMoving;
    public int roundsBeforeCooldownStill;

    private GameObject bulletInstance;
    private float angleDispersionStore;
    private float timeUntilMoveStore;
    private Quaternion bulletRotation;
    private float cooldownMovingTimerStore;
    private float cooldownStillTimerStore;
    private float innerCooldownMovingTimerStore;
    private float innerCooldownStillTimerStore;
    private int roundsBeforeCooldownMovingStore;
    private int roundsBeforeCooldownStillStore;
    private Vector3 offset = new Vector3(0.25f, 0, 0);

    void Fire()
    {
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
        bulletInstance.gameObject.layer = 11;
    }
    void Fire(Vector3 positionOffset)
    {
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position + positionOffset, bulletRotation);
        bulletInstance.gameObject.layer = 11;
    }

    void FirePatternMoving()
    {
        innerCooldownMovingTimer -= Time.deltaTime;
        if(roundsBeforeCooldownMoving >= 0 && innerCooldownMovingTimer <= 0)
        {
            for(angleDispersion = 111; angleDispersion <= 249; angleDispersion += angleDispersionStore)
            {
                bulletRotation = Quaternion.identity;
                bulletRotation.eulerAngles = new Vector3(0, 0, angleDispersion);
                Fire();
            }
            roundsBeforeCooldownMoving--;
            innerCooldownMovingTimer = innerCooldownMovingTimerStore;
            angleDispersion = angleDispersionStore;
        }

        if(roundsBeforeCooldownMoving < 0)
        {
            cooldownMovingTimer = cooldownMovingTimerStore;
        }
    }

    void FirePatternStill()
    {
        innerCooldownStillTimer -= Time.deltaTime;
        if (innerCooldownStillTimer <= 0 && roundsBeforeCooldownStill >= 0)
        {
            Fire(offset);
            Fire(-offset);
            cooldownStillTimer = cooldownMovingTimerStore;
            roundsBeforeCooldownStill--;
        }

        if(roundsBeforeCooldownStill < 0)
        {
            cooldownStillTimer = cooldownStillTimerStore;
        }
    }

	// Use this for initialization
	void Start () {
    angleDispersionStore = angleDispersion;
    timeUntilMoveStore = timeUntilMove;
    cooldownMovingTimerStore = cooldownMovingTimer;
    cooldownStillTimerStore = cooldownStillTimer;
    innerCooldownMovingTimerStore = innerCooldownMovingTimer;
    innerCooldownStillTimerStore = innerCooldownStillTimer;
    roundsBeforeCooldownMovingStore = roundsBeforeCooldownMoving;
    roundsBeforeCooldownStillStore = roundsBeforeCooldownStill;
}
	
	// Update is called once per frame
	void Update () {
        if(enemyMovementScript.isMoving)
        {
            cooldownMovingTimer -= Time.deltaTime;
            if(cooldownMovingTimer <= 0)
            {
                FirePatternMoving();
            }
        }
        else
        {
            cooldownStillTimer -= Time.deltaTime;
            timeUntilMove -= Time.deltaTime;
            if(cooldownStillTimer <= 0)
            {
                FirePatternStill();
                cooldownStillTimer = cooldownStillTimerStore;
            }
            
            //I don't think I quite need this on this particular script?
            //if(timeUntilMove <= 0)
            //{
            //    moveToNextPos(nextPos);
            //}
        }
	}
}
