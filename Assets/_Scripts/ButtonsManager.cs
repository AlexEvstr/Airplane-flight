using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private SpriteRenderer _plane;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private Sprite[] _planeSprites;


    private void Start()
    {
        _background.sprite = _backgroundSprites[int.Parse(PlayerPrefs.GetString("backgroundSelected", "0"))];
        _plane.sprite = _planeSprites[int.Parse(PlayerPrefs.GetString("planeSelected", "0"))];
    }

    public void ReplayBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene("Menu");
    }
}
