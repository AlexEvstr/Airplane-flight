using System.Collections;
using TMPro;
using UnityEngine;

public class CatcherCollision : MonoBehaviour
{
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private GameAudio _gameAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            ChangeSize();
            Destroy(collision.gameObject);
            ScoreCounter.Balance += 10 * LevelController.CurrentLevel;
            PlayerPrefs.SetFloat("balance", ScoreCounter.Balance);
            _balanceText.text = ScoreCounter.Balance.ToString();
            _gameAudio.PlayCoinSound();
        }
    }

    private void ChangeSize()
    {
        StartCoroutine(IncreaseYSize());
    }


    private IEnumerator IncreaseYSize()
    {
        transform.localScale = new Vector2(transform.localScale.x, 0.2f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector2(transform.localScale.x, 0.1f);
    }
}