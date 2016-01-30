using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

    public float timeUntilExplosion;
    public GameObject explodedBullet;

    GameObject explodeBulletInstance;

    private Quaternion bulletRotation;

    public void explode()
    {
        for(float i = 0; i <= 360; i += 22.5f)
        {
            bulletRotation = Quaternion.identity;
            bulletRotation.eulerAngles = new Vector3(0, 0, i);
            explodeBulletInstance = (GameObject)Instantiate(explodedBullet, transform.position, bulletRotation);
        }
        Destroy(explodeBulletInstance, 3f);
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
        Invoke("explode", timeUntilExplosion);
	}
}
