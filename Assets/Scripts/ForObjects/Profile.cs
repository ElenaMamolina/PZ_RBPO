using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Profile : Initialization
{
    private string _name;
    private string _surname;
    private string _middlename;
    private string _gender;
    private string _power;
    private string _age;
    private string _spec;
    [SerializeField] private Text nameText, surnameText, middlenameText, genderText, powerText,ageText,specText;
    [SerializeField] private GameObject panel;
    [SerializeField] private InputField inputField;
    [SerializeField] private Image profileImg;


    public void UpdateProfile(string name, string surname, string middlename, string gender, string power, string age, string spec)
    {
        _name = name;
        _surname = surname;
        _middlename = middlename;
        _gender = gender;
        _power = power;
        _spec = spec;
        _age = age;
        UpdateUIPRofile();
        UpdatePicture();
    }
    public void UpdateUIPRofile() 
    {
        nameText.text = _name;
        surnameText.text = _surname;
        middlenameText.text = _middlename;
        genderText.text = _gender;
        powerText.text = _power;
        ageText.text = _age;
        specText.text = _spec;
    }
    public void LoadPhoto()
    {
        StartCoroutine(updaterProfile.UpdateUrl(inputField.text.ToString()));
        UpdatePicture();
        HideEditPanel();
    }
    public void Show—hangePanel()
    {
        panel.SetActive(true);
    }
    public void HideEditPanel()
    {
        inputField.text = "";
        panel.SetActive(false);
    }
    private void UpdatePicture()
    {
        GameObject.FindGameObjectWithTag("App").GetComponent<LoaderPhotoUrl>().LoadingPhoto(inputField.text, profileImg);
    }

}
