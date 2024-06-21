using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _lineWithArea;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _winText;

    public static float Balance;

    public static int WinAmount;
    public static int Score;
    private int _goalToNextLevel;

    private void Start()
    {
        WinAmount = 0;
        Score = PlayerPrefs.GetInt("currentScore", 0);
        Balance = PlayerPrefs.GetFloat("balance", 0);
        _moneyText.text = Balance.ToString();
        _goalToNextLevel = LevelController.CurrentLevel * 1000;
        _scoreText.text = $"{Score}/{_goalToNextLevel}";
    }

    public void StartIncreasingScore()
    {
        StartCoroutine(IncreaseScore());
    }

    private IEnumerator IncreaseScore()
    {
        while(_lineWithArea != null)
        {
            _scoreText.text = $"{Score}/{_goalToNextLevel}";
            if (Score >= _goalToNextLevel)
            {
                Score = 0;
                LevelController.CurrentLevel++;
                PlayerPrefs.SetInt("levelIndex", LevelController.CurrentLevel);
                _goalToNextLevel = LevelController.CurrentLevel * 1000;
            }
            yield return new WaitForSeconds(0.05f);
            PlayerPrefs.SetInt("currentScore", Score);
            WinAmount++;
            Score++;
        }
        if (!PlaneMovement.isWin)
        {
            Score = 0;
            WinAmount = 0;
            _scoreText.text = $"{Score}/{_goalToNextLevel}";
            PlayerPrefs.SetInt("currentScore", Score);
        }
        Balance += WinAmount;
        _winText.text = WinAmount.ToString();
        PlayerPrefs.SetFloat("balance", Balance);



    }
}