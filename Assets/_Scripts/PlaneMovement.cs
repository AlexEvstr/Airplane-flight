using UnityEngine;
using System.Collections;

public class PlaneMovement : MonoBehaviour
{
    private Vector2 startPoint = new Vector2(-2, -1);
    private Vector2 endPoint = new Vector2(1, 2);
    private float duration = 5f; // Время, за которое самолет достигнет конечной точки
    private float arcHeight = 0.5f; // Максимальная высота дуги
    private float bounceSpeed = 0.2f; // Скорость движения вверх-вниз

    private float minY = -0.7f; // Нижняя граница
    private float maxY = 4f; // Верхняя граница

    void Start()
    {
        StartCoroutine(FlyAlongArc());
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

        // Переход к осциллирующему движению без рывков
        StartCoroutine(OscillateVertically());
    }

    IEnumerator OscillateVertically()
    {
        float elapsedTime = 0f;
        float initialY = transform.position.y;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            float normalizedSin = Mathf.Sin(elapsedTime * bounceSpeed * Mathf.PI * 2);
            float y = Mathf.Lerp(minY, maxY, (normalizedSin + 1f) / 2f);

            // Обеспечиваем плавный переход, начиная осцилляцию от текущей позиции
            float currentY = Mathf.Lerp(initialY, y, elapsedTime / duration);

            transform.position = new Vector3(endPoint.x, currentY, transform.position.z);

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
}
