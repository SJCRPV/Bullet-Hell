using UnityEngine;
using System.Collections.Generic;
using System;

public class ExplodeDownwards : MonoBehaviour {

    [SerializeField]
    private GameObject explodedBulletPrefab;
    [SerializeField]
    private float timeBeforeExplosion;

    private GameObject explodedBulletInstance;
    private SpriteRenderer spriteRenderer;
    private bool isInverted = true;

    public void setInverted()
    {
        Debug.Log(isInverted);
        isInverted = !isInverted;
        Debug.Log("isInverted has changed! It used to be " + !isInverted + " and it is now: " + isInverted);
    }

    private void explode()
    {
        float heightRatio = Math.Abs(spriteRenderer.bounds.min.y / spriteRenderer.bounds.max.y);
        //Debug.Log(heightRatio);
        Quaternion tempRot = Quaternion.identity;
        tempRot.eulerAngles = new Vector3(0, 0, 180);

        if (!isInverted)
        {
            for (float verticalPos = spriteRenderer.bounds.max.y, horizontalPos = spriteRenderer.bounds.min.x; horizontalPos < spriteRenderer.bounds.max.x && verticalPos > spriteRenderer.bounds.min.y; horizontalPos += heightRatio, verticalPos -= heightRatio)
            {
                //Debug.Log("verticalPos: " + verticalPos);
                //Debug.Log("horizontalPos: " + horizontalPos);
                explodedBulletInstance = (GameObject)Instantiate(explodedBulletPrefab, new Vector3(horizontalPos + 0.25f, verticalPos - 0.25f, transform.position.z), tempRot);
            } 
        }
        else
        {
            for (float verticalPos = spriteRenderer.bounds.max.y, horizontalPos = spriteRenderer.bounds.max.x; horizontalPos > spriteRenderer.bounds.min.x && verticalPos > spriteRenderer.bounds.min.y; horizontalPos -= heightRatio, verticalPos -= heightRatio)
            {
                //Debug.Log("verticalPos: " + verticalPos);
                //Debug.Log("horizontalPos: " + horizontalPos);
                explodedBulletInstance = (GameObject)Instantiate(explodedBulletPrefab, new Vector3(horizontalPos - 0.25f, verticalPos - 0.25f, transform.position.z), tempRot);
            }
        }

        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeBeforeExplosion -= Time.deltaTime;
        if(timeBeforeExplosion <= 0)
        {
            explode();
        }
	}

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //isInverted = true;
    }
}
