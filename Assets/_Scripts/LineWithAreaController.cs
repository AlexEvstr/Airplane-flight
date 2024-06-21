using UnityEngine;
using System.Collections.Generic;

public class LineWithAreaController : MonoBehaviour
{
    public Transform startPoint; // Точка с координатами (-11, -5)
    public Transform planeTransform; // Самолет
    public float arcHeight = 2f; // Высота дуги
    [SerializeField] private Material _fillMaterial;

    private LineRenderer lineRenderer;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        if (lineRenderer == null || meshFilter == null || meshRenderer == null || startPoint == null || planeTransform == null)
        {
            Debug.LogError("Не удалось найти необходимые компоненты!");
            return;
        }

        
        meshRenderer.material = _fillMaterial;

        lineRenderer.positionCount = 50; // Установите достаточное количество сегментов для плавной дуги
    }

    void Update()
    {
        DrawLineAndArea();
    }

    void DrawLineAndArea()
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i <= 50; i++)
        {
            float t = i / 50f;
            Vector3 point = GetArcPosition(t);
            points.Add(point);
        }

        lineRenderer.SetPositions(points.ToArray());

        CreateMesh(points);
    }

    Vector3 GetArcPosition(float t)
    {
        float x = Mathf.Lerp(startPoint.position.x, planeTransform.position.x, t);
        float y = Mathf.Lerp(startPoint.position.y, planeTransform.position.y, t) - arcHeight * Mathf.Sin(Mathf.PI * t);

        // Убедитесь, что дуга не опускается ниже начальной точки
        y = Mathf.Max(y, startPoint.position.y);

        return new Vector3(x, y, 0);
    }

    void CreateMesh(List<Vector3> points)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[points.Count * 2];
        int[] triangles = new int[(points.Count - 1) * 6];

        for (int i = 0; i < points.Count; i++)
        {
            vertices[i * 2] = points[i];
            vertices[i * 2 + 1] = new Vector3(points[i].x, startPoint.position.y, points[i].z);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int ti = i * 6;
            int vi = i * 2;
            triangles[ti] = vi;
            triangles[ti + 1] = vi + 2;
            triangles[ti + 2] = vi + 1;
            triangles[ti + 3] = vi + 1;
            triangles[ti + 4] = vi + 2;
            triangles[ti + 5] = vi + 3;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
    }
}
