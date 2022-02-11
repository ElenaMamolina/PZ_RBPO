using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System.Linq;
using System.Collections.Generic;


public class FirebaseMessagingManager : MonoBehaviour, IMessagingManager
{
    private GameObject PrefabSMS;
    private Transform ContentForSms;
    public long IdMessage { get; set; }
    public string NameRec { get; set; }
    public string UrlRec { get; set; }

    public IEnumerator LoadAllMessages(ErrorManager errorManager, GameObject _prefabSMS, Transform contentForSms, string idRecipient, string nameRec, string urlRec)
    {
        var DBTask1 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("Messages").Child(idRecipient).GetValueAsync();
        var DBTask2 = FirebaseConnectionManager.Reference.Child("Users").Child(idRecipient).Child("Messages").Child(FirebaseConnectionManager.User.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted && DBTask2.IsCompleted);

        if (DBTask1.Exception != null && DBTask2.Exception != null)
        {
            errorManager.UpdateTextError("Ошибка!");
        }
        else if (DBTask1.Result.Value == null && DBTask1.Result.Value == null)
        {
            errorManager.UpdateTextError("Данных нет!");
        }
        else
        {
            DataSnapshot snapshot1 = DBTask1.Result;
            DataSnapshot snapshot2 = DBTask2.Result;
            List<DataSnapshot> maschild = new List<DataSnapshot>();
            maschild.AddRange(snapshot1.Children);
            maschild.AddRange(snapshot2.Children);
            IEnumerable<DataSnapshot> query = maschild.OrderBy(masc => int.Parse(masc.Key));
            query.Reverse();
            for (int i = 0; i < maschild.Count; i++)
            {
                GameObject scoreboardElement = Instantiate(_prefabSMS, contentForSms);
                scoreboardElement.GetComponent<Message>().UpdateSMS(query.ToList()[i].Child("name").Value.ToString(), query.ToList()[i].Child("message").Value.ToString(), query.ToList()[i].Child("time").Value.ToString(), query.ToList()[i].Child("url").Value.ToString());
            }
            PrefabSMS = _prefabSMS;
            ContentForSms = contentForSms;
            NameRec = nameRec;
            UrlRec = urlRec;
            IdMessage = maschild.Count;
            errorManager.UpdateTextError("");

        }
    }

    public void ReceiveMessages(string idRecipient)
    {
        FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("Messages").Child(idRecipient).ValueChanged += HandleValueChanged;
    }
    public void NotReceiveMessages(string idRecipient)
    {
        FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("Messages").Child(idRecipient).ValueChanged -= HandleValueChanged;
    }

    public IEnumerator SendMessage(ErrorManager errorManager, GameObject _prefabSMS, string message, Transform contentForSms, string idRecipient, string time)
    {
        var DBTask1 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("Messages").Child(idRecipient).GetValueAsync();
        var DBTask2 = FirebaseConnectionManager.Reference.Child("Users").Child(idRecipient).Child("Messages").Child(FirebaseConnectionManager.User.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted && DBTask2.IsCompleted);

        if (DBTask1.Exception != null && DBTask2.Exception != null)
        {
            errorManager.UpdateTextError("Ошибка!");
        }
        else
        {
            DataSnapshot snapshot1 = DBTask1.Result;
            DataSnapshot snapshot2 = DBTask2.Result;
            IdMessage = snapshot1.ChildrenCount + snapshot2.ChildrenCount;
            StartCoroutine(UpdateMessagesDatabase(_prefabSMS, contentForSms, idRecipient, IdMessage, message, time));
        }
    }



    private string pastMessage = "";
    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            return; 
        }
        
        DataSnapshot snapshot = args.Snapshot;
        if (snapshot.Child((IdMessage).ToString()).Value == null)
        {
            return;
        }
        if (snapshot.Child((IdMessage).ToString()).Child("message").Value.ToString() != pastMessage)
        {
            pastMessage = snapshot.Child((IdMessage).ToString()).Child("message").Value.ToString();
            AddMessageToThePanel(PrefabSMS, ContentForSms, snapshot.Child((IdMessage).ToString()).Child("message").Value.ToString(), NameRec, snapshot.Child((IdMessage).ToString()).Child("time").Value.ToString(), UrlRec);
        }
    }

    public void AddMessageToThePanel(GameObject _prefabSMS, Transform contentForSms, string messageRec, string nameRec, string timeRec, string urlRec)
    {
        GameObject scoreboardElement = Instantiate(_prefabSMS, contentForSms);
        scoreboardElement.GetComponent<AudioSource>().Play();
        scoreboardElement.GetComponent<Message>().UpdateSMS(nameRec, messageRec,timeRec,urlRec);
    }

    private IEnumerator UpdateMessagesDatabase( GameObject _prefabSMS,Transform contentForSms, string idRecipient, long id ,string message,string time)
    {
        var DBTask1 = FirebaseConnectionManager.Reference.Child("Users").Child(idRecipient).Child("Messages").Child(FirebaseConnectionManager.User.UserId).Child(id.ToString()).Child("message").SetValueAsync(message);
        var DBTask2 = FirebaseConnectionManager.Reference.Child("Users").Child(idRecipient).Child("Messages").Child(FirebaseConnectionManager.User.UserId).Child(id.ToString()).Child("time").SetValueAsync(time);
        var DBTask3 = FirebaseConnectionManager.Reference.Child("Users").Child(idRecipient).Child("Messages").Child(FirebaseConnectionManager.User.UserId).Child(id.ToString()).Child("name").SetValueAsync(FirebaseConnectionManager.User.DisplayName);
        var DBTask4 = FirebaseConnectionManager.Reference.Child("Users").Child(idRecipient).Child("Messages").Child(FirebaseConnectionManager.User.UserId).Child(id.ToString()).Child("url").SetValueAsync(FirebaseConnectionManager.User.PhotoUrl.ToString());
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted&& DBTask2.IsCompleted && DBTask3.IsCompleted && DBTask4.IsCompleted);
        GameObject scoreboardElement = Instantiate(_prefabSMS, contentForSms);
        scoreboardElement.GetComponent<Message>().UpdateSMS(FirebaseConnectionManager.User.DisplayName, message, time, FirebaseConnectionManager.User.PhotoUrl.ToString());
        IdMessage++;
    }

    
}


