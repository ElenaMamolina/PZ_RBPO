using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase;


public class ErrorManager : MonoBehaviour
{
    [SerializeField] protected Text errorText;
    public void UpdateTextError(string text)
    {
        errorText.text = text;
    }
   
}
