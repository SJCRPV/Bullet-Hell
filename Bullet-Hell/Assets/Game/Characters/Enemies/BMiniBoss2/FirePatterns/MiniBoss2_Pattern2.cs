using UnityEngine;
using System.Collections;
using System;

public class MiniBoss2_Pattern2 : MonoBehaviour, IFire {

    [SerializeField]
    private GameObject slicePrefab;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float cooldownTimer;
    [SerializeField]
    private float shotgunBulletSpread;
    [SerializeField]
    private float timeBetweenShells;
    [SerializeField]
    private int shotgunAngleSpread;
    [SerializeField]
    private int bulletsPerShotgunShot;

    private GameObject bulletInstance;
    private GameObject sliceInstance;
    private iTweenPath path;
    private Movement_Generic genericMovementScript;
    private Movement_Boss bossMovementScript;
    private Vector3 firstPosition;
    private float cooldownTimerStore;
    private int startingShotgunAngle;

    private void setBulletPath(iTweenPath path, int incrementMult)
    {
        //Debug.Log("This is path: " + path.pathName);
        //Debug.Log("This is incrementMult " + incrementMult);
        //Debug.Log("This is shotgunBulletSpread " + shotgunBulletSpread);
        path.nodes[0] = transform.position;
        path.nodes[1] = new Vector3(transform.position.x, transform.position.y + (shotgunBulletSpread * incrementMult / 4), transform.position.z);
        Vector3[] temp = { path.nodes[0], path.nodes[1] };
        iTween.MoveTo(bulletInstance, iTween.Hash("path", temp, "speed", bulletInstance.GetComponent<MoveForward>().getBulletSpeed() / 4, "easetype", iTween.EaseType.easeOutQuart));
    }

    private void fireShotgun(Vector3 newEulerAngles)
    {
        Quaternion tempRot = Quaternion.identity;
        tempRot.eulerAngles = newEulerAngles;
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, tempRot);
    }

    private void adjustStartingAngle(int counter)
    {
        switch (counter)
        {
            case 0:
                startingShotgunAngle -= shotgunAngleSpread;
                break;
            case 1:
                startingShotgunAngle += shotgunAngleSpread * 2;
                break;
            case 2:
                startingShotgunAngle -= shotgunAngleSpread;
                break;
            default:
                Debug.LogError("Unexpected number. I received " + counter);
                break;
        }
    }

    private IEnumerator unloadShell()
    {
        int counter = 0;
        while (counter < 3)
        {
            iTweenPath[] path = new iTweenPath[30];
            float tempSpread = shotgunBulletSpread;
            //Debug.Log("tempSpread is: " + tempSpread);
            for (int i = 0, bulletCounter = 0; i < bulletsPerShotgunShot; i++)
            {
                //Right
                fireShotgun(new Vector3(0, 0, startingShotgunAngle + shotgunAngleSpread));
                path[bulletCounter] = bulletInstance.GetComponent<iTweenPath>();
                path[bulletCounter].pathName = path[bulletCounter].pathName + bulletCounter;
                setBulletPath(path[bulletCounter++], i);

                //Left
                fireShotgun(new Vector3(0, 0, startingShotgunAngle - shotgunAngleSpread));
                path[bulletCounter] = bulletInstance.GetComponent<iTweenPath>();
                path[bulletCounter].pathName = path[bulletCounter].pathName + bulletCounter;
                setBulletPath(path[bulletCounter++], i);

                //Front
                fireShotgun(new Vector3(0, 0, startingShotgunAngle));
                path[bulletCounter] = bulletInstance.GetComponent<iTweenPath>();
                path[bulletCounter].pathName = path[bulletCounter].pathName + bulletCounter;
                setBulletPath(path[bulletCounter++], i);

                shotgunBulletSpread = tempSpread;
            }

            adjustStartingAngle(counter++);
            yield return new WaitForSeconds(timeBetweenShells);
        }
    }

    private void wiggleToPos(Vector2 target)
    {

    }

    private IEnumerator fireSlice(bool inverted)
    {
        //TODO: Make the boss wiggle left and right with each slice using the wiggleToPos function
        int counter = 0;
        while (counter < 3)
        {
            Vector3 temp = transform.position;
            temp.y -= 1;
            Quaternion tempRot = Quaternion.identity;
            if (inverted)
            {
                tempRot.eulerAngles = new Vector3(0, 0, 50);
            }
            else
            {
                tempRot.eulerAngles = new Vector3(0, 0, -50);
            }

            sliceInstance = (GameObject)Instantiate(slicePrefab, temp, tempRot);
            Debug.Log("inverted is: " + inverted);
            if (inverted)
            {
                sliceInstance.GetComponent<ExplodeDownwards>().setInverted();
            }

            counter++;
            inverted = !inverted;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void firePattern()
    {
        //Debug.Log(bossMovementScript.getCurrentNodeTrioInUse());
        //If at the frontmost position
        //FIX: As expected, this creates all 3 slices virtually instantly. Find a way to delay them enough that they're created seperately.
        //IDEA: Have the slices *slowly* move downwards before exploding. That way you can keep instantiating the slices in front of the boss
        if(bossMovementScript.getCurrentNodeTrioInUse() == 2)
        {
            //Debug.Log("FireFireFire!");
            //Vector3 temp = transform.parent.position;
            //fireSlice(false);
            //Vector3.Lerp(temp, new Vector3(temp.x - 0.25f, temp.y), Time.deltaTime * 2);
            //fireSlice(true);
            //Vector3.Lerp(transform.parent.position, new Vector3(temp.x + 0.25f, temp.y), Time.deltaTime * 2);
            //fireSlice(false);
            //Vector3.Lerp(transform.parent.position, temp, Time.deltaTime * 2);
            StartCoroutine("fireSlice", false);
            StopCoroutine("fireSlice");
        }
        else
        {
            //Debug.Log("Shotguuuuun Blast!");
            //Debug.Log("startingShotgunAngle is: " + startingShotgunAngle);
            //Debug.Log("shotgunAngleSpread is: " + shotgunAngleSpread);
            StartCoroutine("unloadShell", startingShotgunAngle);
            StopCoroutine("unloadShell");
        }
        bossMovementScript.setIsMoving();
    }

    public void assignMovement()
    {
        genericMovementScript = gameObject.GetComponentInParent<Movement_Generic>();
        bossMovementScript = gameObject.GetComponentInParent<Movement_Boss>();
        if(genericMovementScript == null)
        {
            Debug.LogError("genericMovementScript is empty. Did not get the component from parent");
        }
        if(bossMovementScript == null)
        {
            Debug.LogError("bossMovementScript is empty. Did not get the component from parent");
        }
    }

    // Use this for initialization
    void Start () {
        path = gameObject.GetComponent<iTweenPath>();
        cooldownTimerStore = cooldownTimer;
        firstPosition = path.nodes[0];
        assignMovement();
        startingShotgunAngle = 180;
	}
	
    //FIX: Timers don't reset when Boss moves.
	// Update is called once per frame
	void Update () {
        if (genericMovementScript.getIsMoving() == false && bossMovementScript.getIsMoving() == false)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = cooldownTimerStore;
        }

        if (cooldownTimer <= 0)
        {
            firePattern();
            cooldownTimer = cooldownTimerStore;
        }
    }
}
