using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentBetText;
    [SerializeField] private TMP_Text _balanceText;
    private float _currentBet;
    private float _balance;
    private float _winAmount;

    private void Start()
    {
        _currentBet = PlayerPrefs.GetFloat("CurrentBet", 100);
        _currentBetText.text = _currentBet.ToString();
        _balance = PlayerPrefs.GetFloat("balance", 1000);
        _balanceText.text = _balance.ToString();
    }

    public void IncreaseBet()
    {
        if (_currentBet < 1000)
        {
            _currentBet += 10;
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
        _currentBetText.text = _currentBet.ToString();
    }

    public void DecreaseBet()
    {
        if (_currentBet > 10)
        {
            _currentBet -= 10;
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
        _currentBetText.text = _currentBet.ToString();
    }

    public void WinBehavior()
    {
        _winAmount = Coefficient.CurrentCoefficient * _currentBet;
        _balance += _winAmount;
        PlayerPrefs.SetFloat("balance", _balance);
    }

    public void StartBetDecreaseBalance()
    {
        _balance -= _currentBet;
        _balanceText.text = _balance.ToString();
        PlayerPrefs.SetFloat("balance", _balance);
    }
}