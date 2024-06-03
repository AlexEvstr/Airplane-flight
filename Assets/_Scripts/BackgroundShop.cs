using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundShop : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private float _price;
    [SerializeField] private GameObject _priceGroup;
    [SerializeField] private GameObject _purchased;
    [SerializeField] private GameObject _selected;
    [SerializeField] private GameObject _flag;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundSprites;

    private float _money;

    private void Start()
    {
        _money = PlayerPrefs.GetFloat("balance", 1000);
        _moneyText.text = _money.ToString("f0");

        if (PlayerPrefs.GetString("background" + gameObject.name, "") != "")
        {
            _priceGroup.SetActive(false);
            _purchased.SetActive(true);
            if (PlayerPrefs.GetString("backgroundSelected", "0") == gameObject.name)
            {
                _flag.transform.SetParent(transform);
                RectTransform parentRect = GetComponent<RectTransform>();
                RectTransform childRect = _flag.GetComponent<RectTransform>();
                childRect.anchorMin = new Vector2(1, 1);
                childRect.anchorMax = new Vector2(1, 1);
                childRect.pivot = new Vector2(1, 1);
                childRect.anchoredPosition = new Vector2(50, 50);
            }
        }
    }

    public void PickThisBG()
    {
        if (PlayerPrefs.GetString("background" + gameObject.name, "") == "")
        {
            if (_money >= _price)
            {
                _money -= _price;
                _moneyText.text = _money.ToString("f0");
                PlayerPrefs.SetFloat("balance", _money);
                PlayerPrefs.SetString("background" + gameObject.name, "purchased");
                _priceGroup.SetActive(false);
                _selected.SetActive(true);
            }
            else
            {
                return;
            }
        }
        _flag.transform.SetParent(transform);
        RectTransform parentRect = GetComponent<RectTransform>();
        RectTransform childRect = _flag.GetComponent<RectTransform>();
        childRect.anchorMin = new Vector2(1, 1);
        childRect.anchorMax = new Vector2(1, 1);
        childRect.pivot = new Vector2(1, 1);
        childRect.anchoredPosition = new Vector2(50, 50);
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
            if (PlayerPrefs.GetString("background" + gameObject.name, "") != "" || gameObject.name == "0")
            {
                _selected.SetActive(false);
                _purchased.SetActive(true);
            }
        }
    }
}