using UnityEngine;
using System.Collections;

public class GrazePattern : MonoBehaviour {

    public GameObject bulletPrefab;
    public float cooldownTimer;
    //The smaller the number, the bigger the spacing
    public float innerCooldownTimer;

    private float cooldownTimerStore;
    private GameObject bulletInstance;
    private Quaternion bulletRotation;
    private Vector3 correction;
    private float innerCooldownTimerStore;

    void Fire()
    {
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position + correction, new Quaternion(0, 0, 180, 0));
    }

    void FirePattern()
    {
        for (float i = transform.position.x - 0.5f; i < transform.position.x + 0.5f;)
        {
            innerCooldownTimer -= Time.deltaTime;
            if (innerCooldownTimer <= 0)
            {
                correction.x += 0.1f;
                i += 0.1f;
                Fire();
            }
        }
        cooldownTimer = cooldownTimerStore;
    }

	// Use this for initialization
	void Start () {
        cooldownTimerStore = cooldownTimer;
        innerCooldownTimerStore = innerCooldownTimer;
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            FirePattern();
        }
	}
}
