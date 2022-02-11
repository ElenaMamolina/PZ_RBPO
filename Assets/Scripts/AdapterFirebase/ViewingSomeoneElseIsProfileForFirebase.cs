using System.Collections;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;


public class ViewingSomeoneElseIsProfileForFirebase : MonoBehaviour, IWatcher
{
    public IEnumerator LoadUserData(ErrorManager errorManager, string id, params Text[] text)
    {
        var DBTask = FirebaseConnectionManager.Reference.Child("Users").Child(id).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            errorManager.UpdateTextError("Ошибка загрузки данных");
        }
        else if (DBTask.Result.Value == null)
        {
            errorManager.UpdateTextError("данные отстутсвуют");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            text[0].text = snapshot.Child("name").Value.ToString();
            text[1].text = snapshot.Child("surname").Value.ToString();
            text[2].text = snapshot.Child("middlename").Value.ToString();
            text[3].text = snapshot.Child("gender").Value.ToString();
            text[4].text = snapshot.Child("power").Value.ToString() + " " + snapshot.Child("spec").Value.ToString();
            text[5].text = snapshot.Child("age").Value.ToString();
            text[6].text = snapshot.Child("condition").Value.ToString();
            text[7].text = snapshot.Child("url").Value.ToString();
            if (text[6].text == "offline")
            {
                text[6].color = Color.red;
            }
            if (text[6].text == "online")
            {
                text[6].color = Color.blue;
            }
            errorManager.UpdateTextError("");
        }
    }

}
