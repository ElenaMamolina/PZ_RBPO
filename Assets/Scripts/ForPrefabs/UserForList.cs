using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class UserForList : MonoBehaviour
{
    [SerializeField] private Text nameText, surnameText, powerText;
    [SerializeField] private ChatBuilder chatBuilder;
    [SerializeField] private Image photoUrl;
    public void LoadingUserInfo(string name, string surname, string power,string id, string url)
    {
        nameText.text = name;
        surnameText.text = surname;
        powerText.text = power;
        chatBuilder.Id = id;
        chatBuilder.Name = name;
        chatBuilder.Url = url;
        GameObject.FindGameObjectWithTag("App").GetComponent<LoaderPhotoUrl>().LoadingPhoto(url, photoUrl);
    }
}
