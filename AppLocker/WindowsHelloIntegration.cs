using Windows.Security.Credentials.UI;

namespace AppLocker;


public class WindowsHelloIntegration
{
    public static async Task<bool> IsWindowsHelloAvailableAsync()
    {
        var result = await UserConsentVerifier.CheckAvailabilityAsync();
        return result == UserConsentVerifierAvailability.Available;
    }
    public static async Task<bool> AuthenticateUserAsync()
    {
        bool isAvailable = await IsWindowsHelloAvailableAsync();
        if (!isAvailable)
        {
            return false;
        }

        try
        {
            var consentResult = await UserConsentVerifier.RequestVerificationAsync("Please verify your identity");
            return consentResult == UserConsentVerificationResult.Verified;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} : {ex.StackTrace}");
        }

        return false;
    }
}