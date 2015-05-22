using UnityEngine;
using System.Collections;

public class Boss1Ballistics : MonoBehaviour {
	
	public float cooldownTimer;
	public int cooldownRoundLimiter;
	public float innerCooldownTimer;

	private Boss1_Pattern1 boss1_Pattern1Script;
	private float cooldownTimerStore;
	private Vector3 offset = new Vector3(0, 0.5f, 0);
	private float innerCooldownTimerStore;
	private int cooldownRoundLimiterStore;
	
	void Fire()
	{
		boss1_Pattern1Script.Fire();
	}

	void FirePattern()
	{
		innerCooldownTimer -= Time.deltaTime;
		if(innerCooldownTimer <= 0)
		{
			Fire();
			cooldownRoundLimiter--;
			innerCooldownTimer = innerCooldownTimerStore;
			if(cooldownRoundLimiter <= 0)
			{
				cooldownRoundLimiter = cooldownRoundLimiterStore;
				cooldownTimer = cooldownTimerStore;
			}
		}
	}

	// Use this for initialization
	void Start () {
		cooldownTimerStore = cooldownTimer;
		innerCooldownTimerStore = innerCooldownTimer;
		cooldownRoundLimiterStore = cooldownRoundLimiter;
		boss1_Pattern1Script = GetComponent<Boss1_Pattern1>();
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
