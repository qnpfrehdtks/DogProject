using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE_TYPE
{
    IDLE, // 가만히 서있다.
    ACCELATING, // 가속
    GALLOP, // 질주
    KNOCK_BACK, // 넉백
    COLLISION, // 충돌
    SLIDING // 넘어짐
}
 public abstract class State
{

    public STATE_TYPE _stateType;

    public abstract void StartAction(MovingObject mObject);

    public abstract void ExcuteAction(MovingObject mObject);

    public abstract void EndAction(MovingObject mObject);

}


public class IdleState : State
{
    public override void StartAction(MovingObject mObject)
    {
        Debug.Log("Idle로");

        mObject.PlayAnimation(STATE_TYPE.IDLE);
        
    }

    public override void ExcuteAction(MovingObject mObject)
    {
        if (mObject.Controller._pressConditions())
        {
            mObject.ChangeState(STATE_TYPE.ACCELATING);
        }
    }

    public override void EndAction(MovingObject mObject)
    {

    }
}

public class RunState : State
{
    public override void StartAction(MovingObject mObject)
    {
        Debug.Log("Run로");
        mObject.PlayAnimation(STATE_TYPE.ACCELATING);
    }

    public override void ExcuteAction(MovingObject mObject)
    {
        mObject.Controller.PressScreen(Input.mousePosition);

        // 스크린에 손을 떼면
         if (mObject.Controller._releaseConditions())
        {
            mObject.Controller.ReleaseScreen();
            mObject.ChangeState(STATE_TYPE.IDLE);
        }

         // 최대 스피드에 달하면
         if (mObject.ViewModel.CheckMaxVelocity())
        {
            Debug.Log("MAx 도달");
            mObject.ChangeState(STATE_TYPE.GALLOP);
        }
    }

    public override void EndAction(MovingObject mObject)
    {

    }
}

public class GallopState : State
{
    public override void StartAction(MovingObject mObject)
    {
        Debug.Log("Gallop로");
        mObject.PlayAnimation(STATE_TYPE.ACCELATING);
    }

    public override void ExcuteAction(MovingObject mObject)
    {
        if (mObject.Controller._releaseConditions())
        {
            mObject.Controller.ReleaseScreen();
            mObject.ChangeState(STATE_TYPE.IDLE);
        }
    }

    public override void EndAction(MovingObject mObject)
    {

    }
}



