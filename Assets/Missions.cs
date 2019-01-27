using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
        return Player.instance.currentItem != null;
    }
}

public class TrainingMissionDropItem : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(2, 4);
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        GameController.instance.StopBubble();
    }

    override public bool Complete(MonoBehaviour something)
    {
        return Player.instance.currentItem == null;
    }
}

public class MissionFitTV : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(3, 4);
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        GameController.instance.StopBubble();
    }

    override public bool Complete(MonoBehaviour something)
    {
        return Item.fit.ContainsKey(Scenario.instance.tv_id) && Item.fit[Scenario.instance.tv_id];
    }
}

public class MissionCleanFirstRoom : Mission
{
    override public void OnInit(MonoBehaviour something)
    {
        base.OnInit(something);
        GameController.instance.StartBubble(4, 4);
    }
    override public void OnFinish(MonoBehaviour something)
    {
        base.OnFinish(something);
        GameController.instance.StopBubble();
    }

    override public bool Complete(MonoBehaviour something)
    {
        foreach(var id in Scenario.instance.first_items)
        {
            if (!Item.IsFit(id) /*&& (Item.ItemsLocation[id] == null || true)*/)
                return false;
        }
        return true;
    }
}


