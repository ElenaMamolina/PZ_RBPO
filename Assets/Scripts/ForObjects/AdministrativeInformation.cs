using UnityEngine;
using UnityEngine.UI;
public class AdministrativeInformation : Initialization
{

    [SerializeField] private Text textInfo;

    public void DownloadAdministrativeInformation()
    {
        StartCoroutine(administrativeInformationProvider.DownloadAdministrativeInformation(errorManager, textInfo));
    }
}
