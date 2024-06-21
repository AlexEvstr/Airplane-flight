using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentLevelText;
    public static int CurrentLevel;
    private int _highestLevel;

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt("levelIndex", 1);
        _highestLevel = PlayerPrefs.GetInt("highestLevelUnlocked", 1);
    }

    private void Update()
    {
        _currentLevelText.text = $"Lvl : {CurrentLevel}";
        if (_highestLevel < CurrentLevel)
        {
            _highestLevel = CurrentLevel;
            PlayerPrefs.SetInt("highestLevelUnlocked", _highestLevel);
        }
    }
}