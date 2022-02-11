using System.Collections;
public interface ILogger
{
    public IDataProvider dataProvider { get; set; }
    public IEnumerator RegisterPlayer(Profile profile, ErrorManager errorManager, UIManager uIManager, string name, string surname, string middlename, string gender, string power, string age, string spec, string email, string password, string confirm);
}
