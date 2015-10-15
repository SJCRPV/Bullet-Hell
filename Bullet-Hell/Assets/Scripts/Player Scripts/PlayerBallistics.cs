using UnityEngine;
using System.Collections;

public class PlayerBallistics : MonoBehaviour {
	
	PlayerSpawn playerSpawnScript;

	public GameObject bulletPrefab;
    public float bombCooldownTimer;
	public float cooldownTimer;

	float cooldownTimerStore;
	Vector3 verticalOffset = new Vector3(0, 0.5f, 0);
	Vector3 bulletPosition;
	
	private GameObject bulletInstance;
	private Transform objectParent;
	private GameObject extraBulletSource1;
	private GameObject extraBulletSource2;
	private GameObject extraBulletSource3;
	private GameObject extraBulletSource4;

    void adjustPower()
    {
        if (playerSpawnScript.power <= 1)
        {
            extraBulletSource1.SetActive(false);
            extraBulletSource2.SetActive(false);
            extraBulletSource3.SetActive(false);
            extraBulletSource4.SetActive(false);
        }
        else if (playerSpawnScript.power > 1 && playerSpawnScript.power <= 2)
        {
            extraBulletSource1.SetActive(true);
            extraBulletSource2.SetActive(false);
            extraBulletSource3.SetActive(false);
            extraBulletSource4.SetActive(false);
        }
        else if (playerSpawnScript.power > 2 && playerSpawnScript.power <= 3)
        {
            extraBulletSource1.SetActive(true);
            extraBulletSource2.SetActive(true);
            extraBulletSource3.SetActive(false);
            extraBulletSource4.SetActive(false);
        }
        else if (playerSpawnScript.power > 3 && playerSpawnScript.power <= 4)
        {
            extraBulletSource1.SetActive(true);
            extraBulletSource2.SetActive(true);
            extraBulletSource3.SetActive(true);
            extraBulletSource4.SetActive(false);
        }
        else if (playerSpawnScript.power > 4)
        {
            extraBulletSource1.SetActive(true);
            extraBulletSource2.SetActive(true);
            extraBulletSource3.SetActive(true);
            extraBulletSource4.SetActive(true);
        }
    }

	void fire()
	{
		bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position + verticalOffset, Quaternion.identity);
		cooldownTimer = cooldownTimerStore;
		bulletInstance.gameObject.layer = 10;
	}
	void firePattern()
	{
		fire();
	}

	// Use this for initialization
	void Start () {
		cooldownTimerStore = cooldownTimer;
		playerSpawnScript = GetComponentInParent<PlayerSpawn>();
        extraBulletSource1 = GameObject.Find("ExtraBulletSource");
        extraBulletSource2 = GameObject.Find("ExtraBulletSource2");
        extraBulletSource3 = GameObject.Find("ExtraBulletSource3");
        extraBulletSource4 = GameObject.Find("ExtraBulletSource4");
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
        adjustPower();
		if(cooldownTimer <= 0 && Input.GetButton("Fire1"))
		{
			firePattern();
		}
        if(bombCooldownTimer <= 0 && Input.GetButton("Fire2"))
        {
            if(playerSpawnScript.power >= 1)
            {
                //Fire the bomb
            }
        }
	}
}
