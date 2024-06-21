using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Button _startBtn;
    [SerializeField] private GameObject _redLine;
    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while(_redLine != null)
        {
            if (!_startBtn.enabled)
            {  
                GameObject coin = Instantiate(_coinPrefab);
                coin.transform.position = new Vector2(Random.Range(-2f, 2f), 6);
                Destroy(coin, 5);
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        }
    }
}