using UnityEngine;
using UnityEngine.UI;

public class CatcherMovement : MonoBehaviour
{
    private float _speed = 20.0f;
    [SerializeField] private Button _startBtn;
    [SerializeField] private GameObject _redLine;
    private void FixedUpdate()
    {
        if (!_startBtn.enabled && _redLine != null)
        {
            transform.Translate(Input.acceleration.x * Time.deltaTime * _speed, 0, 0);
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}