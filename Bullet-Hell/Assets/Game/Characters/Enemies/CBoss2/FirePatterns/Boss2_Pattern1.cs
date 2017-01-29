using UnityEngine;
using System.Collections;

public class Boss2_Pattern1 : MonoBehaviour, IFire 
{
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private float cooldownTimer;
	[SerializeField]
	private float inBetweenTimer;
	
	private GameObject bulletInstance;
	private Movement_Generic genericMovementScript;
	private Movement_Boss bossMovementScript;
	private float cooldownTimerStore;
	private float inBetweenTimerStore;

    private IEnumerator drawStar()
    {
        while(bossMovementScript.getIsMoving())
        {
            inBetweenTimer -= Time.deltaTime;
            if(inBetweenTimer <= 0)
            {
                Debug.Log("Pew");
                bulletInstance = Instantiate(bulletPrefab, transform.position, new Quaternion(0, 0, 180, 0));
                inBetweenTimer = inBetweenTimerStore;
            }
            yield return null;
        }
        StopCoroutine("drawStar");
    }
	
	public void firePattern()
	{
        StartCoroutine("drawStar");
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
		inBetweenTimerStore = inBetweenTimer;
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