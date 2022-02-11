using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{

    protected UIManager uIManager;
    protected ErrorManager errorManager;
    protected Profile profile;
   


    protected IAdministrativeInformationProvider administrativeInformationProvider;
    protected IWatcher watcher;
    protected IAuth auth;
    protected IDatabaseConnector databaseConnector;
    protected ILogger logger;
    protected IProfileLoader profileLoader;
    protected IMessagingManager messagingManag;
    protected IUpdaterProfile updaterProfile;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        errorManager = GetComponent<ErrorManager>();
        profile = GetComponent<Profile>();
       

        databaseConnector = GetComponent<FirebaseConnectionManager>();
        administrativeInformationProvider = GetComponent<AdministrativeInformationFromFirebase>();
        logger = GetComponent<DataLoggerForFirebase>();
        profileLoader = GetComponent<ProfileLoaderFromFirebase>();
        updaterProfile = GetComponent<FirebaseUpdateProfileData>();
        auth = GetComponent<AccountLoginInFirebase>();
        watcher = GetComponent<ViewingSomeoneElseIsProfileForFirebase>();
        messagingManag = GetComponent<FirebaseMessagingManager>();
    }
}
