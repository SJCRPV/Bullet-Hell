//using UnityEngine;
//using System.Collections;
//
//public class SUperSecretMurderousPattern : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		for(; angleDispersion <= 720;)
//		{
//			betweenBulletSpawnTimer -= Time.deltaTime;
//			if(betweenBulletSpawnTimer <= 0)
//			{
//				bulletRotation = Quaternion.identity;
//				bulletRotation.eulerAngles = new Vector3(0, 0, angleDispersion);
//				bulletInstance = Instantiate(bulletPrefab, transform.position, bulletRotation);
//				bulletInstance.gameObject.layer = 11;
//				angleDispersion += angleDispersionStore;
//				if(angleDispersion == 360)
//				{
//					angleDispersion++;
//				}
//				betweenBulletSpawnTimer = betweenBulletSpawnTimerStore;
//			}
//		}
//	}
//}
