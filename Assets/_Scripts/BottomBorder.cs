using UnityEngine;

public class BottomBorder : MonoBehaviour
{
    [SerializeField] private PlaneMovement _planeMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            _planeMovement.LoseGame();
        }
    }
}