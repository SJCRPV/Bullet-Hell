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
        if (transform.position == path.nodes[path.nodeCount - 1])
        {
            setIsMoving(false);
        }
    }

    public override void setPath()
    {
        if(offset >= 1)
        {
            offset *= -1;
        }
        else
        {
            offset *= -1;
            offset++;
        }
        Debug.Log("Offset: " + offset);
        path = spawnPoint.gameObject.GetComponent<iTweenPath>();
        Vector3 temp = path.nodes[path.nodeCount - 1];
        temp.x += scriptCount * (offset * 0.3f);
        path.nodes[path.nodeCount - 1] = temp;
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(spawnPoint.gameObject.GetComponent<iTweenPath>().pathName), "time", speed * 2, "easetype", iTween.EaseType.easeOutSine));

        setIsMoving(true);
    }
    public void setLeavePath()
    {
        path = leavePoint.gameObject.GetComponent<iTweenPath>();
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
