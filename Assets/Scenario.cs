﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Missions;

//класс отвечает за исполнение и переключение миссий.


public class Scenario : MonoBehaviour
{
    public static Scenario instance;

    public List<Mission> missions = new List<Mission>();
    public Mission curMission = null;
    public Mission gameOverMission = null;
    public int curMissionIndex = 0;
    public int tv_id;
    public int[] AllItems = { 1,2,3,4,5,6,7};
    public BoxCollider2D FirstRoom;

    const float FULL_TIME = 3*60.0f;
    const float SMS_START = 4.0f;
    const float SMS_TIME = 10.0f;
    const float SMS_DEADLINE = 7.0f;
    const float SMS_COUNT = 6;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        curMission = gameOverMission;
        curMission.OnInit(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        missions.Add(new TrainingMissionAD());
        missions.Add(new TrainingMissionTakeItem());
        missions.Add(new TrainingMissionDropItem());
        missions.Add(new MissionFitTV());
//        missions.Add(new MissionCleanFirstRoom());
        missions.Add(new MissionCleanAll());
        //        missions.Add(new TrainingMissionWS());
        missions.Add(new MissionWin());
        gameOverMission = new MissionGameOver();
        curMission = missions[0];
        curMission.OnInit(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curMission == null)
        {
            curMissionIndex = 0;
            curMission = missions[0];
            curMission.OnInit(this);
        }
        if (curMission.Complete(this))
        {
            Debug.Log(curMission);
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

    public void StartGameOverTimer()
    {
        StartCoroutine(GameOverCoroutine());
    }

    public void StartWin()
    {
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(SMS_DEADLINE);
        GameController.instance.StartBubble(12, 4);
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(SMS_START);
        for (int i=0; i<SMS_COUNT; i++)
        {
            if (!(curMission is MissionWin))
            {
                ///показать i-ю смс
                Debug.Log("sms show");
                yield return new WaitForSeconds(SMS_DEADLINE);
                ///проверить что смс было отвечено
                Debug.Log("sms check");
                yield return new WaitForSeconds(SMS_TIME - SMS_DEADLINE);
            }
        }
        yield return new WaitForSeconds(FULL_TIME - SMS_START - SMS_COUNT* SMS_TIME - 4);
        if (!(curMission is MissionWin))
        {
            GameController.instance.StartBubble(14, 4);
            yield return new WaitForSeconds(4);
            GameOver();
        }
    }


}
