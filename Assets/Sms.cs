using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sms : MonoBehaviour
{
    public List<string> smsMessage = new List<string>();

    void Start()
        {
        smsMessage.Add("Test");
        smsMessage.Add("Test2");
        smsMessage.Add("Test3");

    }

}
