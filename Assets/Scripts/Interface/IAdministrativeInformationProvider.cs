using UnityEngine.UI;
using System.Collections;
public interface IAdministrativeInformationProvider
{
    public IEnumerator DownloadAdministrativeInformation(ErrorManager errorManager, Text textInfo);
}