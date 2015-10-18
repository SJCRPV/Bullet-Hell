using UnityEngine;
using System.Collections;

public class MiniBoss1_Pattern1 : MonoBehaviour {

    public GameObject bulletPrefab;
    public float angleDispersion;
    public float cooldownTimer;
    public float innerCooldownTimer;
    public int cooldownRoundLimiter;

    private GameObject bulletInstance;
    private float angleDispersionStore;
    private float cooldownTimerStore;
    private float innerCooldownTimerStore;
    private int cooldownRoundLimiterStore;

    void Fire()
    {

    }

    void FirePattern()
    {
        innerCooldownTimer -= Time.deltaTime;
        if(innerCooldownTimer <= 0)
        {

        }

    }

	// Use this for initialization
	void Start () {
        angleDispersionStore = angleDispersion;
        cooldownTimerStore = cooldownTimer;
        innerCooldownTimerStore = innerCooldownTimer;
        cooldownRoundLimiterStore = cooldownRoundLimiter;
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
