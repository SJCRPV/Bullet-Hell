using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

    public GameObject spawnPoint;
    public GameObject leavePoint;
    [HideInInspector]
    public iTweenPath path;
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
    public void resetOffset()
    {
        Debug.Log("Offset reset!");
        offset = 1;
    }
    public float getOffset()
    {
        return offset;
    }
    public string toString()
    {
        return gameObject.name;
    }
}
