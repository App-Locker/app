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
            MessageBox.Show("Windows Hello is not available on this device.");
            return false;
        }
        var consentResult = await UserConsentVerifier.RequestVerificationAsync("Please verify your identity");
        return consentResult == UserConsentVerificationResult.Verified;
    }
}