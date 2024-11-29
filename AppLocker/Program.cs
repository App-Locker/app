using AppLocker.database_obj;
using Appwrite.Models;

namespace AppLocker;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Thread appLockerThread = new Thread(AppLocker.StartWatchers);
        appLockerThread.Start();
        Application.Run(new Form1());
        

    }

    // private static async void test()
    // {
    //     BackendClient client = new BackendClient();
    //     Blocked_App app = new Blocked_App("Test", "10");
    //     try
    //     {
    //         var document = await client.CreateDataSetAsync("673f05bc0006477e6b18", "673f05f800123ce12c32", app);
    //         Console.WriteLine("Created document ID: " + document.Id);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine("Failed to create document: " + ex.Message);
    //     }
    // }
}