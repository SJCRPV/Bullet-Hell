using UnityEngine;
using System.Collections;

public class MiniBoss2_Pattern3 : MonoBehaviour, IFire {

	[SerializeField]
	private GameObject laserPrefab;
	[SerializeField]
	private float cooldownTimer;
	[SerializeField]
	private float timeBetweenLasers;
	[SerializeField]
	private int numOfLasers;
	
	private GameObject laserInstance;
	private Movement_Generic genericMovementScript;
	private Movement_Boss bossMovementScript;
	private Quaternion rotToPlayer;
	private float cooldownTimerStore;

    private void enlargeToPlayer()
    {
        Vector3 instancePos = laserInstance.transform.position;
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        Vector3 distance = instancePos - playerPos;
        float angleSign = (instancePos.y < playerPos.y) ? 1.0f : -1.0f;
        Vector3 localScale = laserInstance.transform.localScale;

        instancePos.x += (distance.x/2 * angleSign);
        instancePos.y += (distance.y/2 * angleSign);
        laserInstance.transform.position = instancePos;

        localScale.y *= distance.y * 2;
        laserInstance.transform.localScale = localScale;

        Vector3 spriteSize = laserInstance.GetComponent<SpriteRenderer>().sprite.bounds.size;
        laserInstance.GetComponent<BoxCollider2D>().size = spriteSize;
    }

	private Quaternion determineRotToPlayer()
	{
		Vector2 direction = GameObject.Find("Player").transform.position - transform.position;
		direction.Normalize();
		
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        return targetRotation;
	}
	
	private IEnumerator fire()
	{
		for(int i = 0; i < numOfLasers; i++)
		{
			rotToPlayer = determineRotToPlayer();
			laserInstance = Instantiate(laserPrefab, transform.position, rotToPlayer);
            enlargeToPlayer();
			yield return new WaitForSeconds(timeBetweenLasers);
		}
        StopCoroutine("fire");
	}
	
	public void firePattern()
	{
        StartCoroutine("fire");
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
    void Start () {
		cooldownTimerStore = cooldownTimer;
		assignMovement();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!genericMovementScript.getIsMoving() && !bossMovementScript.getIsMoving())
		{
			cooldownTimer -= Time.deltaTime;
		}
		else
		{
			cooldownTimer = cooldownTimerStore;
		}
		
		if(cooldownTimer <= 0)
		{
			firePattern();
			cooldownTimer = cooldownTimerStore;
		}
	}
}
