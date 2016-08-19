using UnityEngine;
using System.Collections;
using System;

public class SlowHoming : MonoBehaviour {

    private float rotationSpeed;
    private Transform playerTransform;

    public void setRotationSpeed(int newRotSpeed)
    {
        rotationSpeed = newRotSpeed;
    }

    private void facePlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(playerTransform == null)
        {
            try
            {
                playerTransform = GameObject.Find("Player").transform;
            }
            catch{}

        }
        else
        {
            if (transform.position.y >= playerTransform.position.y)
            {
                facePlayer();
            }
        }
	}
}
