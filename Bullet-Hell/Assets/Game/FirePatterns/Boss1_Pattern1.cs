using UnityEngine;
using System.Collections;

public class Boss1_Pattern1 : MonoBehaviour, IFire {

	public GameObject bulletPrefab;
	public float angleDispersion;
	public float cooldownTimer;
	public int roundsBeforeCooldown;
	public float innerCooldownTimer;

	private GameObject bulletInstance;
	private Quaternion bulletRotation;
    private Movement_Boss bossMovementScript;
	private float angleDispersionStore;
	private float cooldownTimerStore;
	private float innerCooldownTimerStore;
	private int roundsBeforeCooldownStore;

	public void fire(int extra)
	{
		for (angleDispersion = 111 - extra; angleDispersion <= 249 + extra; angleDispersion += angleDispersionStore) 
		{
			bulletRotation = Quaternion.identity;
			bulletRotation.eulerAngles = new Vector3(0,0,angleDispersion);
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.parent.position, bulletRotation);
			bulletInstance.gameObject.layer = 11;
            bulletInstance.transform.parent = gameObject.transform;
		}
		angleDispersion = angleDispersionStore;
	}

	public void fire()
	{
		for (angleDispersion = 111; angleDispersion <= 249; angleDispersion += angleDispersionStore) 
		{
			bulletRotation = Quaternion.identity;
			bulletRotation.eulerAngles = new Vector3(0,0,angleDispersion);
			bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.parent.position, bulletRotation);
			bulletInstance.gameObject.layer = 11;
		}
		angleDispersion = angleDispersionStore;
	}

	public void firePattern()
	{
		innerCooldownTimer -= Time.deltaTime;
		if(innerCooldownTimer <= 0)
		{
			if(roundsBeforeCooldown == 2)
			{
				fire(11);
			}
			else if(roundsBeforeCooldown == 1)
			{
				fire (7);
			}
			else
			{
				fire();
			}
			roundsBeforeCooldown--;
			innerCooldownTimer = innerCooldownTimerStore;
			if(roundsBeforeCooldown <= 0)
			{
				roundsBeforeCooldown = roundsBeforeCooldownStore;
				cooldownTimer = cooldownTimerStore;
			}
		}
	}

    public void assignMovement()
    {
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
    }

	// Use this for initialization
	void Start () {
		angleDispersionStore = angleDispersion;
		cooldownTimerStore = cooldownTimer;
		innerCooldownTimerStore = innerCooldownTimer;
		roundsBeforeCooldownStore = roundsBeforeCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
		if(cooldownTimer <= 0)
		{
			firePattern();
		}
	}
}
