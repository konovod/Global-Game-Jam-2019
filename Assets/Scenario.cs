using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Missions;

//класс отвечает за исполнение и переключение миссий.


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
