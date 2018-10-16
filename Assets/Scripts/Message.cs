using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : PathFollower
{
    public void Travel(BezierCurve2D path)
    {
        SetPath(path);
    }

    public string msgName; 
    bool shouldTick = true;

    public override void Tick()
    {
        base.Tick();

        if (!shouldTick) return;

        if (Done())
        {
            shouldTick = false;
            StateMachine.Instance.FinishTask();
        }
    }

}
