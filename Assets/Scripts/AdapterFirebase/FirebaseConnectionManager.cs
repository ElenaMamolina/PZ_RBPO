using Firebase.Auth;
using Firebase;
using Firebase.Database;
using UnityEngine;
public class FirebaseConnectionManager : MonoBehaviour, IDatabaseConnector
{
    public static FirebaseAuth AuthorizationPlayer;
    public static FirebaseUser User;
    public static DatabaseReference Reference;
    public void ConnectDB(ErrorManager errorManager)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                AuthorizationPlayer = FirebaseAuth.DefaultInstance;
                Reference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                errorManager.UpdateTextError("Не удалось разрешить все зависимости Firebase: " + dependencyStatus.ToString() + "! Попробуйте зайти позже!");
            }
        });
    }
    
}
