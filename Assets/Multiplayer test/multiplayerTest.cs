using System.Threading.Tasks;
using TMPro;
using Unity.Multiplayer.Widgets;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class multiplayerTest : MonoBehaviour
{
    private TMP_InputField m_InputField;

    private void OnEnable() => enabled = false;

    // this script should not be used right now


    private void Awake() => enabled = false;

    public async void Real()
    {
        await SetupAsync();
    }

    private async void Start()
    {
        // await SetupAsync();
    }

    internal static async Task SetupAsync()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Initialized Unity Services");
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await AuthenticationService.Instance.UpdatePlayerNameAsync("rgb");
            var name = await AuthenticationService.Instance.GetPlayerNameAsync(false);
            Debug.Log(
                $"Signed in anonymously. Name: {name}. ID: {AuthenticationService.Instance.PlayerId}"
            );
        }

        ServicesInitialized();
    }

    /// <summary>
    /// Called after all services are initialized.
    /// </summary>
    public static void ServicesInitialized()
    {
        WidgetServiceInitialization.ServicesInitialized();
    }
}
