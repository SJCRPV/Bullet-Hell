using UnityEngine;
using System.Collections;
using System;

public class Movement_Generic : Movement {
    public override void moveObject()
    {
        transform.position = Vector3.MoveTowards(transform.position, path[path.Length], speed * Time.deltaTime);

        if(transform.position == path[path.Length])
        {
            setIsMoving(false);
        }
    }

    public override void setPath()
    {
        path = iTweenPath.GetPath(spawnPoint.gameObject.GetComponent<iTweenPath>().pathName);
        path[path.Length].x += scriptCount * offset;

        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(spawnPoint.gameObject.GetComponent<iTweenPath>().pathName), "time", speed * 2, "easetype", iTween.EaseType.easeInQuart, "oncomplete", "moveObject"));

        setIsMoving(true);
    }

    // Use this for initialization
    void Start () {
        scriptCount++;
        setPath();
	}
	
	// Update is called once per frame
	void Update () {
        moveObject();
	}
}
