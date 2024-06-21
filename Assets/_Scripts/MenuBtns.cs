using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBtns : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _levelsWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _shopWindow;

    [SerializeField] private GameObject _planesWindow;
    [SerializeField] private GameObject _backgroundsWindow;

    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private GameObject _tutorialPanel;

    [SerializeField] private GameObject _settingsBtns;
    [SerializeField] private GameObject _privacyWindow;
    [SerializeField] private GameObject _helpWindow;

    private void Start()
    {
        _background.sprite = _backgroundSprites[int.Parse(PlayerPrefs.GetString("backgroundSelected", "0"))];
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void PlayGame()
    {
        _menuWindow.SetActive(false);
        _levelsWindow.SetActive(true);
    }

    public void CloseLevels()
    {
        _levelsWindow.SetActive(false);
        _menuWindow.SetActive(true);
    }

    public void OpenSettings()
    {
        _menuWindow.SetActive(false);
        _settingsWindow.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsWindow.SetActive(false);
        _menuWindow.SetActive(true);
    }

    public void OpenShop()
    {
        _menuWindow.SetActive(false);
        _shopWindow.SetActive(true);
    }

    public void CloseShop()
    {
        _shopWindow.SetActive(false);
        _menuWindow.SetActive(true);
    }

    public void OpenPlanesWindow()
    {
        _shopWindow.SetActive(false);
        _planesWindow.SetActive(true);
    }

    public void ClosePlanesWindow()
    {
        _planesWindow.SetActive(false);
        _shopWindow.SetActive(true);
    }

    public void OpenBackgroundWindow()
    {
        _shopWindow.SetActive(false);
        _backgroundsWindow.SetActive(true);
    }

    public void CloseBackgroundWindow()
    {
        _backgroundsWindow.SetActive(false);
        _shopWindow.SetActive(true);
    }

    public void ResetGameDataBtn()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu");
    }

    public void OpenTutorial()
    {
        _settingsWindow.SetActive(false);
        _tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorialPanel.SetActive(false);
        _settingsWindow.SetActive(true);
    }

    public void OpenPrivacy()
    {
        _settingsBtns.SetActive(false);
        _privacyWindow.SetActive(true);
    }

    public void ClosePrivacy()
    {
        _privacyWindow.SetActive(false);
        _settingsBtns.SetActive(true);
    }

    public void OpenHelp()
    {
        _settingsBtns.SetActive(false);
        _helpWindow.SetActive(true);
    }

    public void CloseHelp()
    {
        _helpWindow.SetActive(false);
        _settingsBtns.SetActive(true);
    }
}