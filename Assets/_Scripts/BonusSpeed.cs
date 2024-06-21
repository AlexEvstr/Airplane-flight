using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Android;

public class BonusSpeed : MonoBehaviour
{
    private string bath;

    private void GetIdfa()
    {
        if (PlayerPrefs.GetInt("idfadata") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled,
                string error) =>
                { bath = advertisingId; });
            PlayerPrefs.SetString("reaction", bath);
        }
    }

    private string season;
    private string perspective;
    public struct userAttributes { }
    public struct appAttributes { }

    async Task InitializeRemoteConfigAsync()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    private void OnEnable()
    {
        GetIdfa();
        Permission.RequestUserPermission(Permission.Camera);
        Screen.orientation = ScreenOrientation.Portrait;
        perspective = PlayerPrefs.GetString("truth", "");
        if (perspective == "childhood")
        {
            SceneManager.LoadScene("Menu");
        }
        else if (perspective == "perception")
        {
            SceneManager.LoadScene("Finish");
        }
    }

    private async Task Start()
    {
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += Favour;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void Favour(ConfigResponse configResponse)
    {
        season = RemoteConfigService.Instance.appConfig.GetString("funeral");
        if (season == "bridge")
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            StartCoroutine(Consign());
        }
    }

    private IEnumerator Consign()
    {
        UnityWebRequest solution = UnityWebRequest.Get(season);
        yield return solution.SendWebRequest();

        string angle = solution.downloadHandler.text;

        if (solution.result == UnityWebRequest.Result.Success)
        {
            if (angle.Contains("Erro"))
            {
                PlayerPrefs.SetString("truth", "childhood");
                SceneManager.LoadScene("Menu");
            }
            else
            {
                PlayerPrefs.SetString("exam", angle);

                PlayerPrefs.SetString("truth", "perception");
                SceneManager.LoadScene("Finish");
            }
        }
        else SceneManager.LoadScene("Menu");
    }
}