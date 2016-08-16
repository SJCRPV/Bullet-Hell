using UnityEngine;
using System.Collections;

public class PlayerBallistics : MonoBehaviour {
	
	Character_Player playerCharacterScript;

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
        if (playerCharacterScript.getPower() <= 1)
        {
            extraBulletSource1.SetActive(false);
            extraBulletSource2.SetActive(false);
            extraBulletSource3.SetActive(false);
            extraBulletSource4.SetActive(false);
        }
        else if (playerCharacterScript.getPower() > 1 && playerCharacterScript.getPower() <= 2)
        {
            extraBulletSource1.SetActive(true);
            extraBulletSource2.SetActive(false);
            extraBulletSource3.SetActive(false);
            extraBulletSource4.SetActive(false);
        }
        else if (playerCharacterScript.getPower() > 2 && playerCharacterScript.getPower() <= 3)
        {
            extraBulletSource1.SetActive(true);
            extraBulletSource2.SetActive(true);
            extraBulletSource3.SetActive(false);
            extraBulletSource4.SetActive(false);
        }
        else if (playerCharacterScript.getPower() > 3 && playerCharacterScript.getPower() <= 4)
        {
            extraBulletSource1.SetActive(true);
            extraBulletSource2.SetActive(true);
            extraBulletSource3.SetActive(true);
            extraBulletSource4.SetActive(false);
        }
        else if (playerCharacterScript.getPower() > 4)
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
		playerCharacterScript = GetComponent<Character_Player>();
        extraBulletSource1 = GameObject.Find("ExtraBulletSource");
        extraBulletSource2 = GameObject.Find("ExtraBulletSource2");
        extraBulletSource3 = GameObject.Find("ExtraBulletSource3");
        extraBulletSource4 = GameObject.Find("ExtraBulletSource4");
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

        //There's likely a way to have adjustPower() only run when the player collects or loses a power block
        adjustPower();

        if (cooldownTimer <= 0 && Input.GetButton("Fire1"))
		{
			firePattern();
		}
        if(bombCooldownTimer <= 0 && Input.GetButton("Fire2"))
        {
            if(playerCharacterScript.getPower() >= 1)
            {
                //Fire the bomb
            }
        }
	}
}
