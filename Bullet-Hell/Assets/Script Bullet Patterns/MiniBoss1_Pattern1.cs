using UnityEngine;
using System.Collections;

public class MiniBoss1_Pattern1 : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject bulletPrefabExplode;
    [HideInInspector]
    public EnemyMovement enemyMovementScript;
    public float angleDispersion;
    public float cooldownTimer;
    public float innerCooldownMovingTimer;
    public float innerCooldownStillTimer;
    public int roundsBeforeCooldown;

    private GameObject bulletInstance;
    private float angleDispersionStore;
    private float cooldownTimerStore;
    private Quaternion bulletRotation;
    private float innerCooldownMovingTimerStore;
    private float innerCooldownStillTimerStore;
    private int roundsBeforeCooldownStore;
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

    void FirePattern()
    {
        innerCooldownMovingTimer -= Time.deltaTime;
        if(innerCooldownMovingTimer <= 0)
        {
            if (enemyMovementScript.isMoving)
            {
                innerCooldownMovingTimer -= Time.deltaTime;
                if (innerCooldownMovingTimer <= 0)
                {
                    for (angleDispersion = 111; angleDispersion <= 249; angleDispersion += angleDispersionStore)
                    {
                        bulletRotation = Quaternion.identity;
                        bulletRotation.eulerAngles = new Vector3(0, 0, angleDispersion);
                        Fire();
                    }
                }
                angleDispersion = angleDispersionStore;
                innerCooldownMovingTimer = innerCooldownMovingTimerStore;
            }
            else
            {
                innerCooldownStillTimer -= Time.deltaTime;
                if (innerCooldownStillTimer <= 0)
                {
                    Fire(offset);
                    Fire(-offset);
                    innerCooldownStillTimer = innerCooldownMovingTimerStore;
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        angleDispersionStore = angleDispersion;
        cooldownTimerStore = cooldownTimer;
        innerCooldownMovingTimerStore = innerCooldownMovingTimer;
        innerCooldownStillTimerStore = innerCooldownStillTimer;
        roundsBeforeCooldownStore = roundsBeforeCooldown;
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            FirePattern();
        }
	}
}
