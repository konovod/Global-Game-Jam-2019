using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс отвечает за исполнение и переключение миссий.

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
        Debug.Log("start1");
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


public class Scenario : MonoBehaviour
{
    public static Scenario instance;

    public List<Mission> missions = new List<Mission>();
    public Mission curMission = null;
    public int curMissionIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        missions.Add(new DemoMission1());
        missions.Add(new DemoMission2());
        missions.Add(new DemoMission1());
        curMission = missions[0];
        curMission.OnInit(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (curMission == null)
            return;
        if(curMission.Complete(this))
        {
            curMission.OnFinish(this);
            curMissionIndex += 1;
            if (curMissionIndex < missions.Count)
            {
                curMission = missions[curMissionIndex];
                curMission.OnInit(this);
            }
            else
                curMission = null;
        }
    }
}
