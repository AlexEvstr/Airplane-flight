using OneSignalSDK;
using UnityEngine;

public class ScoreDecreaser : MonoBehaviour
{
    void Start()
    {
        OneSignal.Initialize("79bb67bd-26a1-46d2-baad-da76b6fb3cc4");

        if (PlayerPrefs.GetInt("HasRequestedPushPermission", 0) == 0)
        {

            RequestPushPermissions();

            PlayerPrefs.SetInt("HasRequestedPushPermission", 1);
            PlayerPrefs.Save();
        }
    }

    async void RequestPushPermissions()
    {
        await OneSignal.Notifications.RequestPermissionAsync(false);
    }
}