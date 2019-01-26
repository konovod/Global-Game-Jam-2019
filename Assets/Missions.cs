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

public class DemoMission1 : TimedMission
{
    override public float TheTime(MonoBehaviour something)
    {
        return 10;
    }

    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        Debug.Log("start11");
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        Debug.Log("end1");
    }
}

public class DemoMission2 : TimedMission
{
    override public float TheTime(MonoBehaviour something)
    {
        return 3;
    }

    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        Debug.Log("start2");
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        Debug.Log("end2");
    }
}


