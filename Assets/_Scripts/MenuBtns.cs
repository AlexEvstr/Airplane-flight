using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtns : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _shopWindow;

    [SerializeField] private GameObject _planesWindow;
    [SerializeField] private GameObject _backgroundsWindow;
    [SerializeField] private GameObject _coefficientsWindow;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
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

    public void OpenCoeffsWindow()
    {
        _shopWindow.SetActive(false);
        _coefficientsWindow.SetActive(true);
    }

    public void CloseCoeffsWindow()
    {
        _coefficientsWindow.SetActive(false);
        _shopWindow.SetActive(true);
    }
}