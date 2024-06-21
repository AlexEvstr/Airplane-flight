using UnityEngine;

public class PrivacyWindow : MonoBehaviour
{
    [SerializeField] private UniWebView webView;
    private string _url = "https://www.freeprivacypolicy.com/live/d832f93f-fff1-4a1f-8e2f-2007480450d0";

    void Start()
    {
        webView.OnPageFinished += OnPageFinished;
        webView.OnShouldClose += OnShouldClose;
        webView.Load(_url);
    }

    private void OnPageFinished(UniWebView webView, int statusCode, string url)
    {
        webView.EvaluateJavaScript(@"
            document.body.style.userSelect = 'none';
            var elements = document.querySelectorAll('a, input, button, textarea, select');
            elements.forEach(function(el) {
                el.onclick = function(e) { e.preventDefault(); };
                el.onmousedown = function(e) { e.preventDefault(); };
                el.onmouseup = function(e) { e.preventDefault(); };
                el.onmouseover = function(e) { e.preventDefault(); };
                el.onmouseout = function(e) { e.preventDefault(); };
                el.onmousemove = function(e) { e.preventDefault(); };
                el.onmousewheel = function(e) { e.preventDefault(); };
                el.onblur = function(e) { e.preventDefault(); };
                el.onfocus = function(e) { e.preventDefault(); };
                el.onchange = function(e) { e.preventDefault(); };
                el.onsubmit = function(e) { e.preventDefault(); };
                el.onreset = function(e) { e.preventDefault(); };
                el.onselect = function(e) { e.preventDefault(); };
                el.oninput = function(e) { e.preventDefault(); };
                el.ondblclick = function(e) { e.preventDefault(); };
                el.ondrag = function(e) { e.preventDefault(); };
                el.ondrop = function(e) { e.preventDefault(); };
                el.onkeypress = function(e) { e.preventDefault(); };
                el.onkeydown = function(e) { e.preventDefault(); };
                el.onkeyup = function(e) { e.preventDefault(); };
            });
        ");
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
            webView.OnPageFinished -= OnPageFinished;
        }
    }
}
