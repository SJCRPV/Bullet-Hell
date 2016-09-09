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
    private int shotgunAngleSpread;
    [SerializeField]
    private float shotgunBulletSpread;
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
        path.nodes[1] = new Vector3(transform.position.x + (shotgunBulletSpread * incrementMult), transform.position.y + (shotgunBulletSpread * incrementMult), transform.position.z);
        Vector3[] temp = { path.nodes[0], path.nodes[1] };
        iTween.MoveTo(bulletInstance, iTween.Hash("path", temp, "time", bulletInstance.GetComponent<MoveForward>().getBulletSpeed(), "easetype", iTween.EaseType.easeOutQuart));
    }

    private void fireShotgun(Vector3 newEulerAngles)
    {
        Quaternion tempRot = Quaternion.identity;
        tempRot.eulerAngles = newEulerAngles;
        bulletInstance = (GameObject)Instantiate(bulletPrefab, transform.position, tempRot);
    }

    private void fireSlice(bool inverted)
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
        sliceInstance.GetComponent<ExplodeDownwards>().setInverted();
    }

    public void firePattern()
    {
        Debug.Log(bossMovementScript.getCurrentNodeTrioInUse());
        //If at the frontmost position
        //FIX: As expected, this creates all 3 slices virtually instantly. Find a way to delay them enough that they're created seperately.
        //IDEA: Have the slices *slowly* move downwards before exploding. That way you can keep instantiating the slices in front of the boss
        if(bossMovementScript.getCurrentNodeTrioInUse() == 2)
        {
            //Debug.Log("FireFireFire!");
            Vector3 temp = transform.parent.position;
            fireSlice(false);
            Vector3.Lerp(temp, new Vector3(temp.x - 0.25f, temp.y), Time.deltaTime * 2);
            fireSlice(true);
            Vector3.Lerp(transform.parent.position, new Vector3(temp.x + 0.25f, temp.y), Time.deltaTime * 2);
            fireSlice(false);
            Vector3.Lerp(transform.parent.position, temp, Time.deltaTime * 2);
        }
        else
        {
            //FIX: After the first time, path values aren't properly assigned to the new bullets. Maybe it's because there might be two bullets with the same name?
            //Debug.Log("Shotguuuuun Blast!");
            Debug.Log("startingShotgunAngle is: " + startingShotgunAngle);
            Debug.Log("shotgunAngleSpread is: " + shotgunAngleSpread);
            iTweenPath[] path = new iTweenPath[30];
            float tempSpread = shotgunBulletSpread;
            for (int i = 0, bulletCounter = 0; i < bulletsPerShotgunShot; i++)
            {
                shotgunBulletSpread = tempSpread;

                //Right
                fireShotgun(new Vector3(0, 0, startingShotgunAngle));
                path[bulletCounter] = bulletInstance.GetComponent<iTweenPath>();
                path[bulletCounter].pathName = path[bulletCounter].pathName + bulletCounter;
                setBulletPath(path[bulletCounter++], i);
                shotgunBulletSpread *= -1;

                //Left
                fireShotgun(new Vector3(0, 0, startingShotgunAngle - shotgunAngleSpread));
                path[bulletCounter] = bulletInstance.GetComponent<iTweenPath>();
                path[bulletCounter].pathName = path[bulletCounter].pathName + bulletCounter;
                setBulletPath(path[bulletCounter++], i);
                shotgunBulletSpread = 0;
                
                //Front
                fireShotgun(new Vector3(0, 0, shotgunBulletSpread + shotgunAngleSpread));
                path[bulletCounter] = bulletInstance.GetComponent<iTweenPath>();
                path[bulletCounter].pathName = path[bulletCounter].pathName + bulletCounter;
                setBulletPath(path[bulletCounter++], i);
            }
        }
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

        if (cooldownTimer <= 0)
        {
            firePattern();
            cooldownTimer = cooldownTimerStore;
        }
    }
}
