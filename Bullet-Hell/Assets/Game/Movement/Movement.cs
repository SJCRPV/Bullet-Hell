using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

    public GameObject spawnPoint;
    public GameObject leavePoint;
    [HideInInspector]
    public Vector3[] path;
    public string pathName;
    public float speed;
    public float offset;
    public float timerUntilObjectLeaves;
    public static int scriptCount;

    private bool isMoving;

    public abstract void setPath();
    //public abstract void moveObject();

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
