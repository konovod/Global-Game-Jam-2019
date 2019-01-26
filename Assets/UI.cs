using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    public GameObject _phone;
    public GameObject _SmsPanel;
    public GameObject _navPanel;
    public bool _messageComing;
    public int _incSmsNumber;

    void Start()
    {
        OpenPhone();
    }


    void Update()
    {

        //SelectedMessage();
        OpenSMS();
        
    }

    public void OpenPhone()
    {

        _phone.SetActive(true);

    }
    

    public void OpenSMS()
    {
        if (_messageComing == true)
        {
            _SmsPanel.SetActive(true);
            _navPanel.SetActive(false);
        }
    }

    public void SelectedMessage()
    {
            if (_incSmsNumber == 0)
            {
                Debug.Log("FirstMes");
            }
            else if (_incSmsNumber == 1)
            {
                Debug.Log("SecondMes");
            }
            else if (_incSmsNumber == 2)
            {
                Debug.Log("ThirdMes");
            }

    }

    public void SelectAnswer(int _answer)
    {
        Debug.Log("working");
        switch (_answer)
        {
            case 0:
                Debug.Log("button 1");
                break;
            case 1:
                Debug.Log("button 2");
                break;
            case 2:
                Debug.Log("button 3");
                break;
            case 3:
                Debug.Log("button 4");
                break;

        }
    }

}
