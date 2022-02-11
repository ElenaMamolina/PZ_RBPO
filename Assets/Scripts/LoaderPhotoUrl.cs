using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class LoaderPhotoUrl : MonoBehaviour
{
    [SerializeField] private Image image;
    private void OnEnable()
    {
        StartCoroutine(PlayerImage(FirebaseConnectionManager.User.PhotoUrl.ToString(), image));
    }
    public void LoadingPhoto(string url, Image photoUrl)
    {
        StartCoroutine(PlayerImage(url, photoUrl));
    }

    private IEnumerator PlayerImage(string url,Image photoUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();
            photoUrl.sprite = Sprite.Create(DownloadHandlerTexture.GetContent(uwr), new Rect(0f, 0f, DownloadHandlerTexture.GetContent(uwr).width, DownloadHandlerTexture.GetContent(uwr).height), new Vector2(0f, 0f));
        }
    }
}
