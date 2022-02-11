using UnityEngine;
using UnityEngine.UI;
public class Authorization : Initialization
{
    [SerializeField] private InputField _inputFieldEmail, _inputFieldPassword;
 
    private void Start()
    {
      
        if (PlayerPrefs.HasKey("email") && PlayerPrefs.HasKey("password"))
        {
            _inputFieldPassword.text = PlayerPrefs.GetString("password");
            _inputFieldEmail.text = PlayerPrefs.GetString("email");
            Invoke("LogIn", 1f);
        }
    }
    public void LogIn()
    {
        StartCoroutine(auth.SignIn(uIManager, errorManager, profile, _inputFieldEmail.text, _inputFieldPassword.text));
    }
}
