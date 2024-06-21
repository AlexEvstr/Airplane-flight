using System.Collections;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _bonuses;
    [SerializeField] private GameObject _lineWithArea;
    [SerializeField] private GameObject _splash;
    [SerializeField] private GameObject _bonus;
    [SerializeField] private GameObject _catcher;

    private GameAudio _gameAudio;
    private float yMin = -0.7f;
    private float yMax = 3.5f;
    private float xBorder = 1.8f;

    private void Start()
    {
        _gameAudio = GetComponent<GameAudio>();
    }
    public void SpawnBonuses()
    {
        StartCoroutine(StartSpawnBonus());
    }

    private IEnumerator StartSpawnBonus()
    {
        while (_lineWithArea != null)
        {
            int randomIndex = Random.Range(0, _bonuses.Length);
            yield return new WaitForSeconds(Random.Range(0.5f, 5f));
            GameObject newBonus = Instantiate(_bonuses[randomIndex]);
            newBonus.transform.position = new Vector2(Random.Range(-xBorder, xBorder), Random.Range(yMin, yMax));
            Destroy(newBonus, 2.66f);
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && _lineWithArea != null)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;

                    if (hitObject.CompareTag("Plus100"))
                    {
                        ScoreCounter.Score += 100;
                        ScoreCounter.WinAmount += 100;
                    }
                    else if (hitObject.CompareTag("Plus500"))
                    {
                        ScoreCounter.Score += 500;
                        ScoreCounter.WinAmount += 500;
                    }
                    else if (hitObject.CompareTag("x2"))
                    {
                        ScoreCounter.Score *= 2;
                        ScoreCounter.WinAmount *= 2;
                    }
                    else if (hitObject.CompareTag("x5"))
                    {
                        ScoreCounter.Score *= 5;
                        ScoreCounter.WinAmount *= 5;
                    }
                    else if (hitObject.CompareTag("x10"))
                    {
                        ScoreCounter.Score *= 10;
                        ScoreCounter.WinAmount *= 10;
                    }

                    else if (hitObject.CompareTag("x2Size"))
                    {
                        ChangeSize();
                    }
                    _gameAudio.PlayBonusSound();
                    GameObject newBonus = Instantiate(_bonus);
                    Destroy(newBonus, 1);
                    GameObject newSplash = Instantiate(_splash);
                    newSplash.transform.position = hitObject.transform.position;
                    Destroy(newSplash, 2);
                    Destroy(hitObject);
                    PlayerPrefs.SetInt("currentScore", ScoreCounter.Score);
                }
            }
        }
    }

    private void ChangeSize()
    {
        StartCoroutine(ChangePlatformSize());
    }

    private IEnumerator ChangePlatformSize()
    {
        float size = 1.0f;
        float sizeBig = 2.0f;
        float increaser = 0.01f;
        while(_catcher.transform.localScale.x < 2.0f)
        {
            _catcher.transform.localScale = new Vector2(size += increaser, _catcher.transform.localScale.y);
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(3.0f);
        while (_catcher.transform.localScale.x > 1.0f)
        {
            _catcher.transform.localScale = new Vector2(sizeBig -= increaser, _catcher.transform.localScale.y);
            yield return new WaitForSeconds(0.001f);
        }
    }
}