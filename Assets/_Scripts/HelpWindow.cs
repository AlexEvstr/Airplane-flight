using UnityEngine;

public class HelpWindow : MonoBehaviour
{
    [SerializeField] private UniWebView webView;
    private string _url = "https://manuelhunt.durablesites.com/?pt=NjY2ODM4MjhlYjFlZDVjM2YzOWRlOWY0OjE3MTgzNTI5NzguMzg3OnByZXZpZXc=";

    void Start()
    {
        webView.OnShouldClose += OnShouldClose;
        webView.Load(_url);
    }

    private bool OnShouldClose(UniWebView webView)
    {
        return false;
    }

    private void OnDestroy()
    {
        if (webView != null)
        {
            webView.OnShouldClose -= OnShouldClose;
        }
    }
}
