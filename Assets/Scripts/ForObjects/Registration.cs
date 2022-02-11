using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase;


public class Registration : Initialization
{
    [SerializeField] private InputField _inputFieldName, _inputFieldSurName, _inputFieldMiddleName, _inputFieldAge, _inputFieldSpec, _inputFieldEmail, _inputFieldPassword, _inputFieldConfirmPassword;
    public const string DEF_URL = "https://mtuci.ru/bitrix/templates/modern_blue_s3/images/mtuci_logo21.png";
    public void Register()
    {
        if (_inputFieldName.text == "")
        {
            errorManager.UpdateTextError("Отсутствует имя");
        }
        else if (_inputFieldSurName.text == "")
        {
            errorManager.UpdateTextError("Отсутствует фамилия");
        }
        else if (_inputFieldMiddleName.text == "")
        {
            errorManager.UpdateTextError("Отсутствует отчество");
        }
        else if (uIManager.ParseGenderCheckBoxToString() == "")
        {
            errorManager.UpdateTextError("Отсутствует пол");
        }
        else if (_inputFieldAge.text == "")
        {
            errorManager.UpdateTextError("Отсутствует возраст");
        }
        else if (_inputFieldSpec.text == "")
        {
            errorManager.UpdateTextError("Отсутствует информация о вас");
        }
        else if (_inputFieldPassword.text != _inputFieldConfirmPassword.text)
        {
            errorManager.UpdateTextError("Пароли не совпадают");
        }
        else
        {
            StartCoroutine(logger.RegisterPlayer(profile,errorManager, uIManager, _inputFieldName.text,
                _inputFieldSurName.text, _inputFieldMiddleName.text,
                uIManager.ParseGenderCheckBoxToString(), uIManager.ParsePowerDropdownToString(),
                _inputFieldAge.text, _inputFieldSpec.text, _inputFieldEmail.text,
                _inputFieldPassword.text, _inputFieldConfirmPassword.text));
        }
    }
}





