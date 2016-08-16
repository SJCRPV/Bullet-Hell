using UnityEngine;
using System.Collections;

public class SlowHoming : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed;

    Transform playerTransform;


    //From the get-go, each missile will track the player's position, but have a slow enough rotation that they won't be able to keep up. After their Y coor becomes equal or lower than the player's, they'll stop tracking.

    private void facePlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update () {

        if(playerTransform == null)
        {
            playerTransform = GameObject.Find("APlayer01").transform;
        }

	    if(transform.position.y >= playerTransform.position.y)
        {
            facePlayer();
        }
	}
}
