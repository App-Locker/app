using System.Collections;
using System.Runtime.CompilerServices;
using Windows.Security.Authentication.Web.Provider;
using AppLocker.database_obj;
using Appwrite;
using Appwrite.Models;
using Appwrite.Services;
using Microsoft.Extensions.Configuration;

namespace AppLocker;

public class BackendClient
{
    private static BackendClient instance = null;
    public static BackendClient Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendClient();
            }
            return instance;
        }
    }
    private static Client client;
    private BackendClient()
    {
        client = CreateClient();
        instance = this;
    }
    public static Client CreateClient()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
            .Build();

        return new Client().SetEndpoint("https://backend.applocker.xyz/v1").SetProject("6729d2a1000ba99558e2")
            .SetKey(config["ApiSettings:ApiKey"]);
    }

    public async Task<Session> createUserSession(string email,string password)
    {
        var account = new Account(client);
        return await account.CreateEmailPasswordSession(email, password);
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

        var database = new Databases(client);
        try
        {
            var document = await database.CreateDocument(
                databaseId,
                collectionId,
                ID.Unique(),
                app.dictionary
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

    public async Task<DocumentList> ReadDataSetAsync(string databaseId, string collectionId,params string[] queries)
    {
        var database = new Databases(client);
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
}