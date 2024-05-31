using UnityEngine;
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
    private float elapsedTime = 0f;
    private bool reachedEndPoint = false;
    private GameObject lineWithArea; // Объект LineWithArea
    private Coroutine randomMovementCoroutine;

    void Start()
    {
        lineWithArea = GameObject.Find("LineWithArea"); // Ищем объект LineWithArea
        StartCoroutine(FlyAlongArc());
        StartCoroutine(RandomTimer());
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

        reachedEndPoint = true;
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
        float waitTime = Random.Range(1f, 10f);
        yield return new WaitForSeconds(waitTime);

        // Прекращение текущего движения и запуск взлета вверх вправо
        StopAllCoroutines();
        StartCoroutine(FlyUpRight());
    }

    IEnumerator FlyUpRight()
    {
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
}
