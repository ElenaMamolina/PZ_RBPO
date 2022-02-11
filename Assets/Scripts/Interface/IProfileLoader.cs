using System.Collections;
using UnityEngine;

public interface IProfileLoader
{
    public IEnumerator DownloaAllProfiles(ErrorManager errorManager, Transform contentForPrefabs, GameObject prefabPanelUser);
}