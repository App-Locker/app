using System.Drawing;
using System.Drawing.Imaging;
using AppLocker.database_obj;
using Appwrite;
using Appwrite.Models;
using Appwrite.Services;
using Microsoft.Extensions.Configuration;
using File = Appwrite.Models.File;

namespace AppLocker;

public class BackendClient
{
    private static BackendClient _instance = null;
    public static BackendClient Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendClient();
            }
            return _instance;
        }
    }
    private bool _isLoggedIn { get; set; }

    public bool isLoggedIn
    {
        get => _isLoggedIn;
        private set
        {
            if (_isLoggedIn != value)
            {
                _isLoggedIn = value;
            }
        }
    }

    private static Client Client;

    public Session Session
    {
        get;
        set;
    }

    private BackendClient()
    {
        Client = CreateClient();
        _instance = this;
    }
    public static Client CreateClient()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
            .Build();

        return new Client().SetEndpoint("https://backend.applocker.xyz/v1").SetProject("6729d2a1000ba99558e2")
            .SetKey(config["ApiSettings:ApiKey"]);
    }

    public static bool isValidSession(Session session)
    {
        if (Instance == null)
            BackendClient.CreateClient();
        Account account = new Account(Client);
        return account.GetSession(session.Id) != null;
    }
    public async Task CreateUserSession(string email,string password)
    {
        try
        {
            if(isLoggedIn) return;
            var account = new Account(Client);
            Session = await account.CreateEmailPasswordSession(email, password);
            isLoggedIn = true;
        }
        catch (AppwriteException e)
        {
            Console.WriteLine(e.Message);
            isLoggedIn = false;
        }
       
        
    }
    public async Task<Document> CreateDataSetAsync(string databaseId, string collectionId, Blocked_App app, params string[] permissions)
    {
        if (string.IsNullOrEmpty(databaseId))
        {
            throw new ArgumentException("Database ID cannot be null or empty", nameof(databaseId));
        }

        if (string.IsNullOrEmpty(collectionId))
        {
            throw new ArgumentException("Collection ID cannot be null or empty", nameof(collectionId));
        }

        if (app == null || app.dictionary.Count == 0)
        {
            throw new ArgumentException("Data cannot be null or empty", nameof(app));
        }

        var database = new Databases(Client);
        try
        {
            var document = await database.CreateDocument(
                databaseId,
                collectionId,
                ID.Unique(),
                app.dictionary,
                permissions.ToList()
            );

            Console.WriteLine("Document created successfully:");
            Console.WriteLine(document.Data);
            return document;
        }
        catch (AppwriteException ex)
        {
            Console.WriteLine($"Appwrite error: {ex.Message}");
            Console.WriteLine($"Response: {ex.Response}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
    public static async Task CreateUserAsync(string email,string password)
    {
        // Initialize Appwrite Users service
        var usersService = new Users(Client);

        try
        {
            // User data
            // Create user
            User createdUser = await usersService.CreateBcryptUser(
                ID.Unique(),
                email,
                password
            );

            // Output created user details
            Console.WriteLine($"User created: {createdUser.Id}, {createdUser.Email}");
        }
        catch (AppwriteException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public async Task<DocumentList> ReadDataSetAsync(string databaseId, string collectionId,params string[] queries)
    {
        var database = new Databases(Client);
        try
        {
            var document = await database.ListDocuments(
                databaseId,
                collectionId,
                queries.ToList()
            );
                
            return document;
        }
        catch (AppwriteException ex)
        {
            Console.WriteLine($"Appwrite error: {ex.Message}");
            Console.WriteLine($"Response: {ex.Response}");
            throw;
        }
    }

    public async Task<File> AddFileToBucket(string bucketID,Icon file,string fileName,params string[] permissions)
    {
        using (Bitmap bitmap = file.ToBitmap())
        {
            // Convert the bitmap to a byte array
            byte[] bitmapBytes = GetBytesFromBitmap(bitmap);

            Storage storage = new Storage(Client);
            return await storage.CreateFile(
                bucketID,
                ID.Unique(),
                InputFile.FromBytes(bitmapBytes,fileName,".ico"),
                permissions.ToList()
            );
        }
       
    }
    static byte[] GetBytesFromBitmap(Bitmap bitmap)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // Save the bitmap to the memory stream in a specific format (e.g., PNG)
            bitmap.Save(ms, ImageFormat.Png);

            // Return the byte array
            return ms.ToArray();
        }
    }
}