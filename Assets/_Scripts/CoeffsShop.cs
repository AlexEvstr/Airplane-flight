using TMPro;
using UnityEngine;

public class CoeffsShop : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private float _price;
    [SerializeField] private GameObject _priceGroup;
    [SerializeField] private GameObject _purchased;
    [SerializeField] private GameObject _selected;
    [SerializeField] private GameObject _flag;

    private float _money;

    private void Start()
    {
        _money = PlayerPrefs.GetFloat("balance", 1000);
        //_money = 10000;
        _moneyText.text = _money.ToString("f0");

        if (PlayerPrefs.GetString("maxCoeff" + gameObject.name, "") != "")
        {
            _priceGroup.SetActive(false);
            _purchased.SetActive(true);
            if (PlayerPrefs.GetString("maxCoeffSelected", "0") == gameObject.name)
            {
                _flag.transform.SetParent(transform);
                _flag.transform.position = new Vector2(transform.position.x + 150, transform.position.y + 250);
            }
        }
    }

    public void PickThisCoeff()
    {
        if (PlayerPrefs.GetString("maxCoeff" + gameObject.name, "") == "")
        {
            if (_money >= _price)
            {
                _money -= _price;
                _moneyText.text = _money.ToString("f0");
                PlayerPrefs.SetFloat("balance", _money);
                PlayerPrefs.SetString("maxCoeff" + gameObject.name, "purchased");
                _priceGroup.SetActive(false);
                _selected.SetActive(true);
            }
            else
            {
                return;
            }
        }
        _flag.transform.SetParent(transform);
        _flag.transform.position = new Vector2(transform.position.x + 150, transform.position.y + 250);
        PlayerPrefs.SetInt("maxCoeff", int.Parse(gameObject.name));
        PlayerPrefs.SetString("maxCoeffSelected", gameObject.name);
    }

    private void Update()
    {
        if (transform.childCount == 5)
        {
            _selected.SetActive(true);
            _purchased.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetString("maxCoeff" + gameObject.name, "") != "" || gameObject.name == "0")
            {
                _selected.SetActive(false);
                _purchased.SetActive(true);
            }
        }
    }
}
