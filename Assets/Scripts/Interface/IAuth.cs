using System.Collections;
public interface IAuth
{
    public IDataProvider dataProvider { get; set; }
    public IEnumerator SignIn(UIManager uIManager, ErrorManager errorManager, Profile profile, string email, string password);
}