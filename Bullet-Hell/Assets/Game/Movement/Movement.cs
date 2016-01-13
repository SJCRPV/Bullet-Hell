using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

    public GameObject spawnPoint;
    public GameObject leavePoint;
    [HideInInspector]
    public Vector3[] path;
    public string pathName;
    public float speed;
    public static float offset;
    public float timerUntilObjectLeaves;
    public static int scriptCount;

    private bool isMoving;

    public abstract void setPath();

    public void setIsMoving(bool state)
    {
        isMoving = state;
    }
    public bool getIsMoving()
    {
        return isMoving;
    }
    public string toString()
    {
        return gameObject.name;
    }
}
