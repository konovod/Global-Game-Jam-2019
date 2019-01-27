using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
//using Missions;

//класс отвечает за исполнение и переключение миссий.

[System.Serializable]
public class Message
{
    public string meassage;
    public string[] variants;
    [Range(1, 3)] public int trueVariant = 1;
    public bool was = false;
}

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

    [Header("Messages")]
    public Message[] messages;
    public Text messageText;
    public Button[] btns;
    public GameObject phone;
    public GameObject alarm;
    public Sprite smsScreen;
    public Sprite homeScreen;
    public GameObject interfacePhone;
    public bool answered;

    [Header("Timer")]
    public Slider slider;

    public const float FULL_TIME = 3*60.0f;
    const float SMS_START = 4.0f;
    const float SMS_TIME = 15.0f;
    const float SMS_DEADLINE = 8.0f;

    private void Awake()
    {
        instance = this;
    }

    public void GameOverNow()
    {
        curMission = gameOverMission;
        GameController.instance.StartBubble(11, -1);
        RestartScene();
    }
    public void GameOver()
    {
        curMission = gameOverMission;
        curMission.OnInit(this);
        RestartScene();
    }

    public void RestartScene()
    {
        StartCoroutine(RestartSceneCoroutine());
    }

    public IEnumerator RestartSceneCoroutine()
    {
        StopCoroutine(GameOverCoroutine());
        phone.SetActive(false);
        yield return new WaitForSeconds(4);
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("SampleScene");
    }

    public IEnumerator Alarm()
    {
        //1
        alarm.SetActive(true);
        yield return new WaitForSeconds(0.125f);
        alarm.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        //2
        alarm.SetActive(true);
        yield return new WaitForSeconds(0.125f);
        alarm.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        //3
        alarm.SetActive(true);
        yield return new WaitForSeconds(0.125f);
        alarm.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        //4
        alarm.SetActive(true);
        yield return new WaitForSeconds(0.125f);
        alarm.SetActive(false);
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            slider.value += 1 * Time.deltaTime;

            if (slider.value >= slider.maxValue)
            {
                slider.value = slider.maxValue;
                break;
            }
            yield return null;
        }
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
        Item.fit.Clear();

        phone.SetActive(false);
        alarm.SetActive(false);
        slider.gameObject.SetActive(false);
        slider.maxValue = FULL_TIME;
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
        messages = messages.OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();
        for (int i=0; i< messages.Length; i++)
        {
            if (!(curMission is MissionWin))
            {
                if (i == 0) GameController.instance.StartBubble(5, 4);

                DisplayMessage(messages[i]);
                answered = false;
                yield return new WaitForSeconds(SMS_DEADLINE);
                if (!answered)
                    GameOver();
                Debug.Log("sms check");
                yield return new WaitForSeconds(SMS_TIME - SMS_DEADLINE);
            }
        }
        yield return new WaitForSeconds(FULL_TIME - SMS_START - messages.Length * SMS_TIME - 4);
        if (!(curMission is MissionWin))
        {
            GameController.instance.StartBubble(14, 4);
            yield return new WaitForSeconds(4);
            GameOver();
        }
    }

    void DisplayMessage(Message message)
    {
        if (curMission is MissionGameOver)
            return;
        messageText.text = message.meassage;

        StartCoroutine(Alarm());

        phone.GetComponent<Image>().sprite = smsScreen;
        interfacePhone.SetActive(true);

        foreach (Button b in btns)
        {
            b.gameObject.SetActive(false);
            b.onClick.RemoveAllListeners();
        }
        for (int i = 0; i < message.variants.Length; i++)
        {
            btns[i].gameObject.SetActive(true);
            btns[i].transform.GetChild(0).GetComponent<Text>().text = message.variants[i];
        }
        btns[0].onClick.AddListener(() => TrueVatiant(1, message.trueVariant));
        if (message.variants.Length < 2)
            return;
        btns[1].onClick.AddListener(() => TrueVatiant(2, message.trueVariant));
        if (message.variants.Length < 3)
            return;
        btns[2].onClick.AddListener(() => TrueVatiant(3, message.trueVariant));
    }

    void TrueVatiant(int currentVariant, int trueVariant)
    {
        if (currentVariant == trueVariant)
        {
            answered = true;
            phone.GetComponent<Image>().sprite = homeScreen;
            interfacePhone.SetActive(false);
            Debug.Log("right");
        }
        else
        {
            GameOverNow();
            Debug.Log("wrong");
        }
        //phone.SetActive(false);
    }
}
