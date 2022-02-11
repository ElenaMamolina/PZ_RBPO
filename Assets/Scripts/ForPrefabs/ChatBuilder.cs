using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBuilder : MonoBehaviour
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    private UIManager uIManager;
    private MessagingManager messagingManager;
    private void OnMouseDown()
    {
        messagingManager = GameObject.FindGameObjectWithTag("App").GetComponent<MessagingManager>();
        messagingManager.Url = Url;
        messagingManager.IdRecipient = Id;
        messagingManager.NameRecipient = Name;
        messagingManager.SubscribeMessages();
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        uIManager.GoToWindow((int)WindowsApp.Messages);
    }
}
