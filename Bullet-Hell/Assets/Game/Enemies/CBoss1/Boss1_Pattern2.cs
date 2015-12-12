using UnityEngine;
using System.Collections;

public class Boss1_Pattern2 : MonoBehaviour {

	public GameObject bulletPrefab;
	public float angleDispersion;
	public float cooldownTimer;
    [HideInInspector]
    public float reverseCooldownTimer;
	public float betweenBulletSpawnTimer;
    [HideInInspector]
    public float reverseBetweenBulletSpawnTimer;
    [HideInInspector]
    public float reverseAngleDispersion;

	private GameObject bulletInstance;
	private float angleDispersionStore;
	private float cooldownTimerStore;
	private float betweenBulletSpawnTimerStore;
	private Quaternion bulletRotation;
	private bool startCooldown;
    private bool startReverseCooldown;

	void Fire(float angle)
	{
		bulletRotation = Quaternion.identity;
		bulletRotation.eulerAngles = new Vector3(0, 0, angle);
		bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, bulletRotation);
		bulletInstance.gameObject.layer = 11;
		bulletInstance.transform.parent = this.transform;
	}

	public void FirePattern()
	{
		if(angleDispersion <= 720)
		{
			betweenBulletSpawnTimer -= Time.deltaTime;
			if(betweenBulletSpawnTimer <= 0)
			{
				angleDispersion += angleDispersionStore;
				Fire (angleDispersion);
				betweenBulletSpawnTimer = betweenBulletSpawnTimerStore;
			}
		}
		if(angleDispersion > 720)
		{
			cooldownTimer = cooldownTimerStore;
			startCooldown = true;
		}
	}

    public void ReverseFirePattern()
    {
        if (reverseAngleDispersion >= -720)
        {
            reverseBetweenBulletSpawnTimer -= Time.deltaTime;
            if (reverseBetweenBulletSpawnTimer <= 0)
            {
                reverseAngleDispersion -= angleDispersionStore;
                Fire(reverseAngleDispersion);
                reverseBetweenBulletSpawnTimer = betweenBulletSpawnTimerStore;
            }
        }
        if (reverseAngleDispersion < -720)
        {
            cooldownTimer = cooldownTimerStore;
            startReverseCooldown = true;
        }
    }

	// Use this for initialization
	void Start () {
		angleDispersionStore = angleDispersion;
		cooldownTimerStore = cooldownTimer;
		betweenBulletSpawnTimerStore = betweenBulletSpawnTimer;
		angleDispersion = 720;
		startCooldown = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(startCooldown)
		{
			cooldownTimer -= Time.deltaTime;
			angleDispersion = 0;
		}
        if(startReverseCooldown)
        {
            reverseCooldownTimer -= Time.deltaTime;
            reverseAngleDispersion = 0;
        }

		if(cooldownTimer <= 0)
		{
			startCooldown = false;
			FirePattern();
            ReverseFirePattern();
		}
	}
}
