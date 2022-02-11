using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Firebase.Database;


public class MessagingManager : Initialization
{
    [SerializeField] private InputField inputField;
    [SerializeField] private GameObject _prefabSMS;
    [SerializeField] private Transform contentForSms;
    public string NameRecipient { get; set; }
    public string IdRecipient { get; set; }
    public string Url { get; set; }


    public void SubscribeMessages()
    {
        StartCoroutine(messagingManag.LoadAllMessages(errorManager, _prefabSMS, contentForSms, IdRecipient, NameRecipient, Url));
        messagingManag.ReceiveMessages(IdRecipient);
    }


    public void UnsubscribeMessages()
    {
        messagingManag.NotReceiveMessages(IdRecipient);
        ClearSMSRecipient();
    }

    public void SendMessage()
    {
        if (inputField.text != "")
        {
            StartCoroutine(messagingManag.SendMessage(errorManager,_prefabSMS, inputField.text, contentForSms, IdRecipient, DateTime.Now.ToString()));
            inputField.text = "";
        }
    }

    private void ClearSMSRecipient()
    {
        for (int i = 0; i < contentForSms.childCount; i++)
        {
            Destroy(contentForSms.GetChild(i).gameObject);
        }
    }

}



