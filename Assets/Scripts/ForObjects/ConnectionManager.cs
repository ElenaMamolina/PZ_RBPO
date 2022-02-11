using UnityEngine;
public class ConnectionManager : Initialization
{
    public void Start()
    {
        databaseConnector.ConnectDB(errorManager);
    }
}
