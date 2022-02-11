using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class FirebaseUpdateProfileData : MonoBehaviour, IUpdaterProfile
{
 
    public IEnumerator UpdateUrl(string url)
    {
        if (FirebaseConnectionManager.User != null)
        {
            UserProfile profile = new UserProfile { PhotoUrl = new System.Uri(url) };
            var profileTask = FirebaseConnectionManager.User.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
            StartCoroutine(UpdateNameUrl(url));
        }
    }
    private IEnumerator UpdateNameUrl(string url)
    {
        var DBTask = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("url").SetValueAsync(url);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }

    private void OnApplicationPause(bool pause)
    {
        if (FirebaseConnectionManager.Reference != null)
        {
            if (pause)
            {
                StartCoroutine(UpdateCondition("offline"));
            }
            else
            {
                StartCoroutine(UpdateCondition("online"));
            }
        }
    }

    public IEnumerator UpdateCondition(string sost)
    {
        var DBTask = FirebaseConnectionManager.Reference.Child("Users").Child(FirebaseConnectionManager.User.UserId).Child("condition").SetValueAsync(sost);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }

}
