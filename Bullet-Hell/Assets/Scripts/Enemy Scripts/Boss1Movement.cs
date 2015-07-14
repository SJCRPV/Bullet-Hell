using UnityEngine;
using System.Collections;

public class Boss1Movement : MonoBehaviour {

	public float speed;

	void moveSelf()
	{
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Boss1Pattern2Path"), "time", speed, "easeType", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.loop));
    }

    void Start()
    {
        moveSelf();
    }
}
