using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RecepientProfile : Initialization
{
    [SerializeField] private Text textName, textSurname, textMiddlename, genderText, ageText, infoText,infoCondition,url;
    [SerializeField] private Image image;
    [SerializeField] private MessagingManager messManager;
    public void LoadProfile()
    {
        StartCoroutine(watcher.LoadUserData(errorManager, messManager.IdRecipient, textName, textSurname, textMiddlename, genderText, ageText, infoText, infoCondition,url));
        GameObject.FindGameObjectWithTag("App").GetComponent<LoaderPhotoUrl>().LoadingPhoto(url.text, image);
    }

  
}
