namespace AppLocker.database_obj;

public class Blocked_App
{
    public Dictionary<string, object> dictionary
    {
        get;
    } = new Dictionary<string, object>();
    public Blocked_App(string title, string userId)
    {
        dictionary.Add("title",title);
        dictionary.Add("user_id",userId);
    }
    
}