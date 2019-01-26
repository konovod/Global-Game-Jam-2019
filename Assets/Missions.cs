using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Mission
{
    virtual public void OnInit(MonoBehaviour something)
    {

    }
    virtual public void OnFinish(MonoBehaviour something)
    {

    }
    abstract public bool Complete(MonoBehaviour something);

}

abstract public class TimedMission : Mission
{
    public float time;
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        time = TheTime(something);
    }

    abstract public float TheTime(MonoBehaviour something);
    override public bool Complete(MonoBehaviour something)
    {
        time -= Time.deltaTime;
        return time <= 0;
    }
}

public class TrainingMissionAD : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(0, 4);
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        GameController.instance.StopBubble();
    }

    override public bool Complete(MonoBehaviour something)
    {
        return Player.instance.UsedAD;
    }
}

public class TrainingMissionWS : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(1, 4);
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        GameController.instance.StopBubble();
    }

    override public bool Complete(MonoBehaviour something)
    {
        return Player.instance.UsedWS;
    }
}


public class TrainingMissionTakeItem : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(1, 4);
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        GameController.instance.StopBubble();
    }

    override public bool Complete(MonoBehaviour something)
    {
        return Player.instance.UsedWS;
    }
}


public class MissionWin : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(12, -1);
    }

    override public bool Complete(MonoBehaviour something)
    {
        return false;
    }
}

public class MissionGameOver : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(11, -1);
    }

    override public bool Complete(MonoBehaviour something)
    {
        return false;
    }
}


