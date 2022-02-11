using System.Collections;
using UnityEngine;
using Firebase.Database;

public class FirebaseDataDownloadManager : MonoBehaviour, IDataProvider
{
    public IEnumerator LoadUserData(ErrorManager errorManager, Profile profile)
    {
        var DBTask = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).GetValueAsync();
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

            profile.UpdateProfile
                (
                snapshot.Child("name").Value.ToString(),
                snapshot.Child("surname").Value.ToString(),
                snapshot.Child("middlename").Value.ToString(),
                snapshot.Child("gender").Value.ToString(),
                snapshot.Child("power").Value.ToString(),
                snapshot.Child("age").Value.ToString(),
                snapshot.Child("spec").Value.ToString()
                );

        }
    }

    public IEnumerator UpdateDataDatabase(Profile profile , string name, string surname, string middlename, string gender, string power, string age, string spec,string url)
    {
        var DBTask1 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("name").SetValueAsync(name);
        var DBTask2 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("surname").SetValueAsync(surname);
        var DBTask3 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("middlename").SetValueAsync(middlename);
        var DBTask4 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("gender").SetValueAsync(gender);
        var DBTask5 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("power").SetValueAsync(power);
        var DBTask6 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("age").SetValueAsync(age);
        var DBTask7 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("spec").SetValueAsync(spec);
        var DBTask8 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("url").SetValueAsync(url);
        var DBTask9 = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("condition").SetValueAsync("online");
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted&&
        DBTask2.IsCompleted&& DBTask3.IsCompleted && DBTask4.IsCompleted 
        && DBTask5.IsCompleted && DBTask6.IsCompleted && DBTask7.IsCompleted
        && DBTask8.IsCompleted && DBTask9.IsCompleted);

        profile.UpdateProfile
            (
             name,
             surname,
             middlename,
             gender,
             power,
             age,
             spec
            );

    }
}


