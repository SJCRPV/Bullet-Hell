using UnityEngine;
using System.Collections;

public class Boss1Movement : Movement {

    public void assignMovement()
    {
    }

	public override void setPath()
	{
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Boss1Pattern2Path"), "time", speed, "easeType", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.loop));
    }

    void Start()
    {
        setPath();
    }
}
