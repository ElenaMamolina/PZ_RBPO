using System.Collections;
using UnityEngine;
using Firebase;
public class AccountLoginInFirebase : MonoBehaviour, IAuth
{
    public IDataProvider dataProvider { get; set; }
    private void Start()
    {
        dataProvider = GetComponent<FirebaseDataDownloadManager>();
    }
    public IEnumerator SignIn(UIManager uIManager, ErrorManager errorManager,Profile profile, string email, string password)
    {
        var loginTask = FirebaseConnectionManager.AuthorizationPlayer.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            GetComponent<ErrorFirebase>().WhatErrorOut(loginTask.Exception.GetBaseException() as FirebaseException);
        }
        else
        {
            PlayerPrefs.SetString("email", email);
            PlayerPrefs.SetString("password", password);
            PlayerPrefs.Save();
            FirebaseConnectionManager.User = loginTask.Result;
            StartCoroutine(dataProvider.LoadUserData(errorManager,profile));
            uIManager.GoToWindow((int)WindowsApp.Profile);
            errorManager.UpdateTextError("");
        }
    }
}
