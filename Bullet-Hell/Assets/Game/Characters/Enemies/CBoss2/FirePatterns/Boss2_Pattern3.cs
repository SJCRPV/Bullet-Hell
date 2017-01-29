using UnityEngine;
using System.Collections;
using System;

public class Boss2_Pattern3 : MonoBehaviour, IFire {

    [SerializeField]
    private GameObject cescentPrefab;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float cooldownTimer;

    private Animation movementAnimation;
    private GameObject crescentInstance;
    private GameObject bulletInstance;
    private float cooldownTimerStore;

    public void firePattern()
    {
        throw new NotImplementedException();
    }

    public void assignMovement()
    {
        throw new NotImplementedException();
    }

	// Use this for initialization
	void Start () {
        cooldownTimerStore = cooldownTimer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
