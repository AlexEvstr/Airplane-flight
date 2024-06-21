using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelInformation : MonoBehaviour
{
    private Button _levelButton;
    private Image _buttonImage;
    private int _levelNumber;

    private void Start()
    {
        InitializeBehavior();
    }

    private void InitializeBehavior()
    {
        _levelButton = GetComponent<Button>();
        _buttonImage = GetComponent<Image>();

        if (_levelButton == null || _buttonImage == null || !int.TryParse(gameObject.name, out _levelNumber))
        {
            return;
        }

        int highestUnlockedLevel = PlayerPrefs.GetInt("highestLevelUnlocked", 1);
        if (highestUnlockedLevel < _levelNumber)
        {
            _levelButton.enabled = false;
            _buttonImage.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            _levelButton.enabled = true;
            _buttonImage.color = new Color(1f, 1f, 1f);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void SelectLevel()
    {
        if (int.TryParse(gameObject.name, out _levelNumber))
        {
            PlayerPrefs.SetInt("levelIndex", _levelNumber);
            SceneManager.LoadScene("Game");
        }
    }
}
