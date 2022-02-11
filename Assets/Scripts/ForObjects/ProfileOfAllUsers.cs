
using UnityEngine;

public class ProfileOfAllUsers : Initialization
{
    [SerializeField] private GameObject prefabPanelUser;
    [SerializeField] private Transform contentForPrefabs;
    public void LoadListProfile()
    {
        StartCoroutine(profileLoader.DownloaAllProfiles( errorManager, contentForPrefabs, prefabPanelUser));
    }
}
