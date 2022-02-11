using System.Collections;
public interface IDataProvider
{
    public IEnumerator LoadUserData(ErrorManager errorManager, Profile profile);
    public IEnumerator UpdateDataDatabase(Profile profile, string name, string surname, string middlename, string gender, string power, string age, string spec, string url);
}
