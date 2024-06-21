using UnityEngine;

public class FinishMenu : MonoBehaviour
{
    [SerializeField] private UniWebView _manager;
    private string _mixture;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        _manager.SetBackButtonEnabled(true);
        _manager.OnShouldClose += (view) => { return false; };
        _mixture = $"https://{PlayerPrefs.GetString("exam", "")}{PlayerPrefs.GetString("reaction", "")}";
        _manager.Load(_mixture);
    }
}