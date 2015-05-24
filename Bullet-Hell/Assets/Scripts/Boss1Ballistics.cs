using UnityEngine;
using System.Collections;

public class Boss1Ballistics : MonoBehaviour {

	private Boss1_Pattern1 boss1_Pattern1Script;
	private Boss1_Pattern2 boss1_Pattern2Script;
	private DamageHandler damageHandlerScript;
	private int bossHP;
	
	void Fire()
	{
		if(damageHandlerScript.getHealthPoints() >= bossHP/2)
		{
			boss1_Pattern1Script.enabled = true;
			boss1_Pattern2Script.enabled = false;
		}
		else if(damageHandlerScript.getHealthPoints() >= bossHP/4 && damageHandlerScript.getHealthPoints() < bossHP/2)
		{
			boss1_Pattern1Script.enabled = false;
			boss1_Pattern2Script.enabled = true;
		}
	}

	// Use this for initialization
	void Start () {
		boss1_Pattern1Script = GetComponent<Boss1_Pattern1>();
		boss1_Pattern2Script = GetComponent<Boss1_Pattern2>();
		damageHandlerScript = GetComponent<DamageHandler>();
		boss1_Pattern1Script.enabled = false;
		boss1_Pattern2Script.enabled = false;
		bossHP = damageHandlerScript.getHealthPoints();
	}
	
	// Update is called once per frame
	void Update () {
		Fire();
	}
}
