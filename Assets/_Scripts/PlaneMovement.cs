using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public Vector2 startPoint = new Vector2(0, 0);
    public Vector2 endPoint = new Vector2(7, 2);
    public float duration = 5f; // Время, за которое самолет достигнет конечной точки
    public float arcHeight = 2f; // Максимальная высота дуги
    public float bounceHeight = 3f; // Амплитуда движения вверх-вниз
    public float bounceSpeed = 1f; // Скорость движения вверх-вниз

    private float elapsedTime = 0f;
    private bool reachedEndPoint = false;

    void Start()
    {
        transform.position = new Vector3(startPoint.x, startPoint.y, transform.position.z);
    }

    void Update()
    {
        if (!reachedEndPoint)
        {
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                Vector2 position = GetConcaveArcPosition(t);
                transform.position = new Vector3(position.x, position.y, transform.position.z);
            }
            else
            {
                reachedEndPoint = true;
                elapsedTime = 0f;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float y = endPoint.y + Mathf.Sin(elapsedTime * bounceSpeed) * bounceHeight;
            transform.position = new Vector3(endPoint.x, y, transform.position.z);
        }
    }

    Vector2 GetConcaveArcPosition(float t)
    {
        float x = Mathf.Lerp(startPoint.x, endPoint.x, t);
        float y = Mathf.Lerp(startPoint.y, endPoint.y, t) - arcHeight * Mathf.Sin(Mathf.PI * t);

        return new Vector2(x, y);
    }
}
