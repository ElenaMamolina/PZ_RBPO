using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _windows; 
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private Text textOptions;
    [SerializeField] private Text specTextPlaceHolder;

    public string ParseGenderCheckBoxToString()
    {
        string gender = "";
        if (toggles[0].isOn == true)
        {
            gender = "муж";
        }
        if (toggles[1].isOn == true)
        {
            gender = "жен";
        }
        return gender;
    }

    public void CheckToogle(int num)
    {
        int t = 0;
        if(num == 1)
        {
            t = 0;
        }
        else
        {
            t = 1;
        }
        if (toggles[num].isOn == true)
        {
            toggles[t].isOn = false;
        }
        else
        {
            toggles[t].isOn = true;
        }
    }
 


    public string ParsePowerDropdownToString()
    {
        string power = "";
        UpdateTextHolder();
        power = textOptions.text;
        return power;
    }
    public void UpdateTextHolder()
    {
        if (textOptions.text == "Студент")
        {
            specTextPlaceHolder.text = "ваш курс";
        }
        if (textOptions.text == "Преподаватель")
        {
            specTextPlaceHolder.text = "ваша степень";
        }
    }
    
    public void GoToWindow(int typeWindow)
    {
        for (int i = 0; i < _windows.Length; i++)
            _windows[i].SetActive(false);
       
        if(typeWindow< _windows.Length)
            _windows[typeWindow].SetActive(true);
        else
            GoToWindow((int)WindowsApp.Authoriztion);
    }
}

