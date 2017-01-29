using UnityEngine;
using System.Collections;

public class PlayerBallistics : MonoBehaviour {
	
	Character_Player playerCharacterScript;

	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
    private float bombCooldownTimer;
	[SerializeField]
	private float cooldownTimer;

	private float cooldownTimerStore;
    private float bombCooldownTimerStore;
	private Vector3 verticalOffset = new Vector3(0, 0.5f, 0);
	private Vector3 bulletPosition;
	private GameObject bulletInstance;
	private Transform objectParent;
	private GameObject extraBulletSource1;
	private GameObject extraBulletSource2;
	private GameObject extraBulletSource3;
	private GameObject extraBulletSource4;

    private void adjustPower()
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
	
	private void fireBomb()
	{
        Debug.Log("Firing bomb!");
		GameObject.Find("Bomb").GetComponent<CircleCollider2D>().enabled = true;
        GameObject.Find("Bomb").GetComponent<SpriteRenderer>().enabled = true;
	}

	private void fire()
	{
		bulletInstance = Instantiate(bulletPrefab, transform.position + verticalOffset, Quaternion.identity);
		cooldownTimer = cooldownTimerStore;
		bulletInstance.gameObject.layer = 10;
	}
	
	private void firePattern()
	{
		fire();
	}

	// Use this for initialization
	private void Start () {
		cooldownTimerStore = cooldownTimer;
        bombCooldownTimerStore = bombCooldownTimer;
		playerCharacterScript = GetComponent<Character_Player>();
        extraBulletSource1 = GameObject.Find("ExtraBulletSource");
        extraBulletSource2 = GameObject.Find("ExtraBulletSource2");
        extraBulletSource3 = GameObject.Find("ExtraBulletSource3");
        extraBulletSource4 = GameObject.Find("ExtraBulletSource4");
	}
	
	// Update is called once per frame
	private void Update () {
		cooldownTimer -= Time.deltaTime;
        bombCooldownTimer -= Time.deltaTime;

        //FIX: There's likely a way to have adjustPower() only run when the player collects or loses a power block
        adjustPower();

        if (cooldownTimer <= 0 && Input.GetButton("Fire1"))
		{
			firePattern();
		}
        if(bombCooldownTimer <= 0 && Input.GetButton("FireBomb"))
        {
            if(playerCharacterScript.getPower() >= 1)
            {
                fireBomb();
                playerCharacterScript.decreasePower(1);
                bombCooldownTimer = bombCooldownTimerStore;
            }
        }
	}
}
