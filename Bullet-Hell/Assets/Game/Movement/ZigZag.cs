using UnityEngine;
using System.Collections;

public class ZigZag : MonoBehaviour {

    static float scriptCount;

    public float radius;
    public float speed;

    private bool restartAndInvertFlag;
    private bool scriptCountIsPair;
    private float centerX;
    private float centerY;
    private double speedScale;
    private float timer;

    private void restartAndInvert()
    {
        timer = 0;
        setCenter();
    }

    private void setCenter()
    {
        centerX = transform.position.x;
        centerY = transform.position.y - radius;
    }

	// Use this for initialization
	void Start () {
        if (scriptCount++ % 2 == 0)
        {
            scriptCountIsPair = true;
        }
        else
        {
            speed *= -1;
        }
        setCenter();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * speed;
        //Debug.Log(Mathf.Sin(timer));

        transform.position = new Vector2((centerX + Mathf.Sin(timer) * radius), (centerY + Mathf.Cos(timer) * radius));
        if (scriptCountIsPair)
        {
            if(!restartAndInvertFlag)
            {
                if(Mathf.Sin(timer) <= 0)
                {
                    //Debug.Log("Swap!");
                    restartAndInvertFlag = true;
                    speed *= -1;
                    restartAndInvert();
                }
            }
            else
            {
                if(Mathf.Sin(timer) >= 0)
                {
                    //Debug.Log("Revert!");
                    restartAndInvertFlag = false;
                    speed *= -1;
                    restartAndInvert();
                }
            }
        }
        else
        {
            if (!restartAndInvertFlag)
            {
                if (Mathf.Sin(timer) >= 0)
                {
                    //Debug.Log("Swap!");
                    restartAndInvertFlag = true;
                    speed *= -1;
                    restartAndInvert();

                }
            }
            else
            {
                if (Mathf.Sin(timer) <= 0)
                {
                    //Debug.Log("Revert!");
                    restartAndInvertFlag = false;
                    speed *= -1;
                    restartAndInvert();
                }
            }
        }
    }
}
