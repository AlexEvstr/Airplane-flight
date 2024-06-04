using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaneMovement : MonoBehaviour
{
    private Vector2 startPoint = new Vector2(-2, -1);
    private Vector2 endPoint = new Vector2(1, 2);
    private float duration = 5f; // Время, за которое самолет достигнет конечной точки
    private float arcHeight = 0.5f; // Максимальная высота дуги

    private float minY = -1f; // Нижняя граница
    private float maxY = 3f; // Верхняя граница
    private float minX = -1f; // Левая граница
    private float maxX = 2f; // Правая граница
    private GameObject lineWithArea; // Объект LineWithArea
    private Coroutine randomMovementCoroutine;

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Coefficient _coefficient;
    [SerializeField] private BettingSystem _bettingSystem;
    [SerializeField] private GameObject _takeOutBtn;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private AudioClip _flyAwaySound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;

    private float _maxTime;
    [SerializeField] private float[] _maxTimes;

    private bool _isWin;

    void Start()
    {
        _maxTime = _maxTimes[int.Parse(PlayerPrefs.GetString("maxCoeffSelected", "0"))];
        _isWin = false;
        lineWithArea = GameObject.Find("LineWithArea"); // Ищем объект LineWithArea
    }


    public void StartBtn()
    {
        GetComponent<AudioSource>().Play();
        foreach (var button in _buttons)
        {
            button.enabled = false;
        }
        _coefficient.StartCounting();
        StartCoroutine(FlyAlongArc());
        StartCoroutine(RandomTimer());
        _takeOutBtn.SetActive(true);
    }

    IEnumerator FlyAlongArc()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            Vector2 position = GetConcaveArcPosition(t);
            transform.position = new Vector3(position.x, position.y, transform.position.z);

            yield return null;
        }

        randomMovementCoroutine = StartCoroutine(RandomMovement());
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            float targetX = Random.Range(minX, maxX);
            float targetY = Random.Range(minY, maxY);
            float moveDuration = Random.Range(0.5f, 2.5f);
            float moveElapsedTime = 0f;

            Vector2 startPosition = transform.position;
            Vector2 targetPosition = new Vector2(targetX, targetY);

            while (moveElapsedTime < moveDuration)
            {
                moveElapsedTime += Time.deltaTime;
                float t = moveElapsedTime / moveDuration;
                Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, t);
                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

                yield return null;
            }
        }
    }

    IEnumerator RandomTimer()
    {
        float waitTime = Random.Range(1f, _maxTime);
        yield return new WaitForSeconds(waitTime);
        _takeOutBtn.SetActive(false);
        StopAllCoroutines();
        ShowLose();
        StartCoroutine(FlyUpRight());
    }

    IEnumerator FlyUpRight()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(_flyAwaySound);
        if (lineWithArea != null)
        {
            Destroy(lineWithArea);
        }

        while (true)
        {
            transform.position += new Vector3(1, 1, 0) * 10 * Time.deltaTime;
            yield return null;
        }
    }

    Vector2 GetConcaveArcPosition(float t)
    {
        float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
        float y = Mathf.Lerp(startPoint.y, endPoint.y, t) - arcHeight * Mathf.Sin(Mathf.PI * t);
        y = Mathf.Max(y, startPoint.y); // Убедитесь, что y не опускается ниже начальной точки

        return new Vector2(x, y);
    }

    private IEnumerator ShowLosePanel()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(_loseSound);
        if (GameAudio.isVibro) Vibration.VibrateNope();
        _losePanel.SetActive(true);
    }

    private void ShowLose()
    {
        StartCoroutine(ShowLosePanel());
    }

    private IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().PlayOneShot(_winSound);
        if (GameAudio.isVibro) Vibration.VibratePeek();
        _winPanel.SetActive(true);
        _bettingSystem.WinBehavior();
    }

    public void ShowWin()
    {
        StopAllCoroutines();
        StartCoroutine(FlyUpRight());
        StartCoroutine(ShowWinPanel());
    }

    private void Update()
    {
        if (BettingSystem.AutoCashOutAmout.ToString("f2") == Coefficient.CurrentCoefficient.ToString("f2") && BettingSystem.Auto && !_isWin)
        {
            ShowWin();
            _isWin = true;
        }
    }
}
