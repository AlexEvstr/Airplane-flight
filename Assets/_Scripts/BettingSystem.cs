using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentBetText;
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private TMP_Text _winAmountText;
    [SerializeField] private Button _manualBtn;
    [SerializeField] private Button _autoBtn;
    [SerializeField] private GameObject _autoCashOut;
    [SerializeField] private TMP_Text _autoCashText;
    private float _currentBet;
    private float _balance;
    private float _winAmount;
    public static float AutoCashOutAmout;
    public static bool Auto;

    private void Start()
    {
        int auto = PlayerPrefs.GetInt("autoBet", 0);
        if (auto == 0) Auto = false;
        else Auto = true;
        
        _balance = PlayerPrefs.GetFloat("balance", 1000);
        _balanceText.text = _balance.ToString("f0");
        _currentBet = PlayerPrefs.GetFloat("CurrentBet", 100);
        if (_currentBet > _balance)
        {
            _currentBet = _balance;
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
        _currentBetText.text = _currentBet.ToString("f0");

        AutoCashOutAmout = PlayerPrefs.GetFloat("autoCashOutAmout", 2.0f);
        _autoCashText.text = AutoCashOutAmout.ToString("f2");
    }

    private void Update()
    {
        if (Auto)
        {
            _autoBtn.GetComponent<Image>().color = Color.red;
            _manualBtn.GetComponent<Image>().color = new Color(1,1,1,1);
            _autoCashOut.SetActive(true);
        }
        else
        {
            _autoBtn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _manualBtn.GetComponent<Image>().color = Color.red;
            _autoCashOut.SetActive(false);
        }
    }

    public void ChooseAutoBtn()
    {
        Auto = true;
        PlayerPrefs.SetInt("autoBet", 1);
    }

    public void ChooseManualBtn()
    {
        Auto = false;
        PlayerPrefs.SetInt("autoBet", 0);
    }

    public void IncreaseBet()
    {
        if (_currentBet < 1000 && _balance > _currentBet)
        {
            _currentBet += 10;
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
        _currentBetText.text = _currentBet.ToString("f0");
    }

    public void DecreaseBet()
    {
        if (_currentBet > 10)
        {
            _currentBet -= 10;
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
        _currentBetText.text = _currentBet.ToString("f0");
    }

    public void WinBehavior()
    {
        _winAmount = Coefficient.CurrentCoefficient * _currentBet;
        _winAmountText.text = _winAmount.ToString("f0");
        _balance += _winAmount;
        _balanceText.text = _balance.ToString("f0");
        PlayerPrefs.SetFloat("balance", _balance);
    }

    public void StartBetDecreaseBalance()
    {
        _balance -= _currentBet;
        _balanceText.text = _balance.ToString("f0");
        PlayerPrefs.SetFloat("balance", _balance);
    }

    public void IncreaseAutoCash()
    {
        if (AutoCashOutAmout < 4.9f)
        {
            AutoCashOutAmout += 0.1f;
            _autoCashText.text = AutoCashOutAmout.ToString("f2");
            PlayerPrefs.SetFloat("autoCashOutAmout", AutoCashOutAmout);
        }
    }

    public void DecreaseAutoCash()
    {
        if (AutoCashOutAmout > 0.1f)
        {
            AutoCashOutAmout -= 0.1f;
            _autoCashText.text = AutoCashOutAmout.ToString("f2");
            PlayerPrefs.SetFloat("autoCashOutAmout", AutoCashOutAmout);
        }
    }

    public void Bet50Coins()
    {
        if (_balance >= 50)
        {
            _currentBet = 50;
            _currentBetText.text = _currentBet.ToString("f0");
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
    }

    public void Bet250Coins()
    {
        if (_balance >= 250)
        {
            _currentBet = 250;
            _currentBetText.text = _currentBet.ToString("f0");
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
    }

    public void Bet500Coins()
    {
        if (_balance >= 500)
        {
            _currentBet = 500;
            _currentBetText.text = _currentBet.ToString("f0");
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
    }

    public void Bet1000Coins()
    {
        if (_balance >= 1000)
        {
            _currentBet = 1000;
            _currentBetText.text = _currentBet.ToString("f0");
            PlayerPrefs.SetFloat("CurrentBet", _currentBet);
        }
    }
}