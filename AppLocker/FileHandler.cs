namespace AppLocker;

public class FileHandler
{
    public static string localDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppLocker");

    public static void init()
    {
        createDir(Path.Combine(localDirectory, "AppLocker"));
    }
    public static StreamWriter CreateFileStream(string filename,params string[] folders)
    {
        if (folders.Length == 0)
        {
            return new StreamWriter(Path.Combine(localDirectory, filename));
        }
        
        string path = Path.Combine(localDirectory, folders[0]);
        createDir(path);
        
        for (int i = 1; i < folders.Length; i++)
        {
            path = Path.Combine(path, folders[i]);
            createDir(path);
        }
        path = Path.Combine(path, filename);
        
        return new StreamWriter(path);
    }

    public static string CreateFilePath(string filename, params string[] folders)
    {
        if (folders.Length == 0)
        {
            return Path.Combine(localDirectory, filename);
        }
        
        string path = Path.Combine(localDirectory, folders[0]);
        createDir(path);
        
        for (int i = 1; i < folders.Length; i++)
        {
            path = Path.Combine(path, folders[i]);
            createDir(path);
        }
        path = Path.Combine(path, filename);
        createDir(path);
        return path;
    }

    public static void createDir(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
    
}