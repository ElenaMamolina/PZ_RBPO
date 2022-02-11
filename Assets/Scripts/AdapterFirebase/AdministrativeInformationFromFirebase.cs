using System.Collections;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class AdministrativeInformationFromFirebase :MonoBehaviour, IAdministrativeInformationProvider
{
    public IEnumerator DownloadAdministrativeInformation(ErrorManager errorManager, Text textInfo)
    {
        var DBTask = FirebaseConnectionManager.Reference.Child("AdminInfo").Child("Info").GetValueAsync();

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
            textInfo.text = snapshot.Value.ToString();
            errorManager.UpdateTextError("");
        }
    }

}
