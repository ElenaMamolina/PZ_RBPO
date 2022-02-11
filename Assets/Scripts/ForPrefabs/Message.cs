using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Message : MonoBehaviour
{
    [SerializeField] private Text textSMS, textData,textNAME;
    [SerializeField] private Image photoUrl;
    public void UpdateSMS(string name,string sms,string time,string url)
    {
        textNAME.text = name;
        textSMS.text = sms;
        textData.text = time;
        GameObject.FindGameObjectWithTag("App").GetComponent<LoaderPhotoUrl>().LoadingPhoto(url, photoUrl);
    }
}
