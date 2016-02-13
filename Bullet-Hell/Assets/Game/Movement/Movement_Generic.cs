using UnityEngine;
using System.Collections;
using System;

public class Movement_Generic : Movement {

    private void selfDestruct()
    {
        Destroy(gameObject);
    }

    public override void setPath()
    {
        //Debug.Log("Offset: " + offset);
        
        //This is how you change an iTween path node value
        path = spawnPoint.gameObject.GetComponent<iTweenPath>();
        Vector3 temp = path.nodes[path.nodeCount - 1];
        temp.x += offset;
        path.nodes[path.nodeCount - 1] = temp;
        //Debug.Log("Coordinates of last node: " + path.nodes[path.nodeCount - 1]);
        
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(spawnPoint.gameObject.GetComponent<iTweenPath>().pathName), "time", speed * 2, "easetype", iTween.EaseType.easeOutSine, "oncomplete", "setIsMoving", "oncompleteparams", false));

        //Revert the value so the next iteration calculates using the right base
        temp.x -= offset;
        path.nodes[path.nodeCount - 1] = temp;

        setIsMoving(true);
        if (offset > 0)
        {
            offset *= -1;
        }
        else
        {
            offset *= -1;
            offset++;
        }
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
