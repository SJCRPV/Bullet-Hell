using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

    
    [HideInInspector]
    public iTweenPath path;
    //I don't think I need _this_ particular pathName...
    //public string pathName;
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
        //Debug.Log("Offset reset!");
        offset = 0;
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
