using UnityEngine;
using System.Collections;
using System;

public class Movement_Generic : Movement {

    private void selfDestruct()
    {
        Destroy(gameObject);
    }

    public void isStillMoving()
    {
        if (transform.position == path[path.Length - 1])
        {
            setIsMoving(false);
        }

    }

    public override void setPath()
    {
        path = iTweenPath.GetPath(spawnPoint.gameObject.GetComponent<iTweenPath>().pathName);
        path[path.Length-1].x += scriptCount * offset;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(spawnPoint.gameObject.GetComponent<iTweenPath>().pathName), "time", speed * 2, "easetype", iTween.EaseType.easeOutSine));

        setIsMoving(true);
    }
    public void setLeavePath()
    {
        path = iTweenPath.GetPath(leavePoint.gameObject.GetComponent<iTweenPath>().pathName);
        path[path.Length - 1].x += scriptCount * offset;
        //iTween.PutOnPath(gameObject, path, 0);
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(leavePoint.gameObject.GetComponent<iTweenPath>().pathName), "time", speed * 2, "easetype", iTween.EaseType.easeOutSine, "oncomplete","selfDestruct"));

        setIsMoving(true);
    }

    // Use this for initialization
    void Start () {
        scriptCount++;
        setPath();
	}
	
	// Update is called once per frame
	void Update () {
        isStillMoving();
        if(getIsMoving() == false)
        {
            timerUntilObjectLeaves -= Time.deltaTime;
        }

        if(timerUntilObjectLeaves <= 0)
        {
            setLeavePath();
            timerUntilObjectLeaves = 999;
        }
	}
}
