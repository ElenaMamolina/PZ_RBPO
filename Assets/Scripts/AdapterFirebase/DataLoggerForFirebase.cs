using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase;



public class DataLoggerForFirebase : MonoBehaviour, ILogger
{
    
    public IDataProvider dataProvider { get; set; }
    private void Start()
    {
        dataProvider = GetComponent<FirebaseDataDownloadManager>();
    }
    public IEnumerator RegisterPlayer(Profile profileMain ,ErrorManager errorManager,UIManager uIManager, string name, string surname, string middlename, string gender, string power, string age, string spec, string email, string password, string confirm)
    {
            var registerTask = FirebaseConnectionManager.AuthorizationPlayer.CreateUserWithEmailAndPasswordAsync(email, password);
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                GetComponent<ErrorFirebase>().WhatErrorOut(registerTask.Exception.GetBaseException() as FirebaseException);
            }
            else
            {
                FirebaseConnectionManager.User = registerTask.Result;

                if (FirebaseConnectionManager.User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = name, PhotoUrl = new System.Uri(Registration.DEF_URL) };

                    var profileTask = FirebaseConnectionManager.User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

                    if (profileTask.Exception != null)
                    {
                        errorManager.UpdateTextError("Ошибка создания профиля!");
                    }
                    else
                    {
                        PlayerPrefs.SetString("email", email);
                        PlayerPrefs.SetString("password", password);
                        PlayerPrefs.Save();
                    StartCoroutine(dataProvider.UpdateDataDatabase(profileMain,name, surname, middlename, gender, power, age, spec, Registration.DEF_URL));
                        uIManager.GoToWindow((int)WindowsApp.Profile);
                        errorManager.UpdateTextError("");
                    }
                }
            }
        
    }


}
