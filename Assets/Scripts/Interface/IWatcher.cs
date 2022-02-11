using System.Collections;
using UnityEngine.UI;
public interface IWatcher
{
    public IEnumerator LoadUserData(ErrorManager errorManager, string id, params Text[] text);
}