using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessagingManager
{
    public IEnumerator LoadAllMessages(ErrorManager errorManager, GameObject _prefabSMS, Transform contentForSms, string idRecipient, string nameRec, string urlRec);
    public IEnumerator SendMessage(ErrorManager errorManager, GameObject _prefabSMS, string message, Transform contentForSms, string idRecipient,string time);
    public void NotReceiveMessages(string idRecipient);
    public void ReceiveMessages(string idRecipient);
    public void AddMessageToThePanel(GameObject _prefabSMS, Transform contentForSms, string messageRec, string nameRec, string timeRec, string urlRec);

}
