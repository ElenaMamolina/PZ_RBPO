using System.Collections;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class ProfileLoaderFromFirebase : MonoBehaviour , IProfileLoader 
{
    public IEnumerator DownloaAllProfiles(ErrorManager errorManager,Transform contentForPrefabs, GameObject prefabPanelUser)
    {
        var DBTask = FirebaseConnectionManager.Reference.Child("Users").OrderByChild("age").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            errorManager.UpdateTextError("Ошибка!");
        }
        else
        {

            DataSnapshot snapshot = DBTask.Result;
            foreach (Transform child in contentForPrefabs.transform)
            {
                Destroy(child.gameObject);
            }
            DataSnapshot[] maschild = new DataSnapshot[snapshot.ChildrenCount];
            int k = 0;
            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                maschild[k] = childSnapshot;
                k++;
            }
            SortBubble(maschild);

            foreach (DataSnapshot childSnapshot in maschild.Reverse())
            {
                if (childSnapshot.Child("name").Value != null)
                {
                    string id = childSnapshot.Key;

                    if (id == FirebaseConnectionManager.User.UserId)
                    {
                        continue;
                    }
                 
                    string name = childSnapshot.Child("name").Value.ToString();
                    string surname = childSnapshot.Child("surname").Value.ToString();
                    string power = childSnapshot.Child("power").Value.ToString();
                    string url = childSnapshot.Child("url").Value.ToString();
                    GameObject scoreboardElement = Instantiate(prefabPanelUser, contentForPrefabs);
                    scoreboardElement.GetComponent<UserForList>().LoadingUserInfo(name, surname, power, id, url );
                }
            }
        }
    }



    public DataSnapshot[] SortBubble(DataSnapshot[] maschild)
    {
        DataSnapshot temp;
        for (int i = 0; i < maschild.Length; i++)
        {
            for (int j = i + 1; j < maschild.Length; j++)
            {
                if (int.Parse(maschild[i].Child("age").Value.ToString()) > int.Parse(maschild[j].Child("age").Value.ToString()))
                {
                    temp = maschild[i];
                    maschild[i] = maschild[j];
                    maschild[j] = temp;
                }
            }
        }
        return maschild;
    }
}