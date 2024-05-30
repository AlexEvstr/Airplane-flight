using UnityEngine;
using System.Collections.Generic;

public class GraphTrailController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Transform planeTransform;
    public int maxPoints = 100;
    public float pointSpacing = 0.1f;

    private List<Vector3> points = new List<Vector3>();
    private Vector3 lastPoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        planeTransform = GameObject.FindWithTag("Plane").transform;

        if (lineRenderer == null || meshFilter == null || meshRenderer == null || planeTransform == null)
        {
            Debug.LogError("Не удалось найти необходимые компоненты!");
            return;
        }

        meshRenderer.material = new Material(Shader.Find("Unlit/Color"));
        meshRenderer.material.color = Color.red;

        lastPoint = planeTransform.position;
    }

    void Update()
    {
        if (Vector3.Distance(lastPoint, planeTransform.position) >= pointSpacing)
        {
            if (planeTransform.position.x != lastPoint.x)
            {
                AddPoint();
            }
            lastPoint = planeTransform.position;
        }

        UpdateLineRenderer();
        CreateMesh();
    }

    void AddPoint()
    {
        if (points.Count >= maxPoints)
        {
            points.RemoveAt(0);
        }

        points.Add(planeTransform.position);
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }

    void CreateMesh()
    {
        if (points.Count < 2)
            return;

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[points.Count * 2];
        int[] triangles = new int[(points.Count - 1) * 6];

        float screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, -Camera.main.transform.position.z)).y;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 point = points[i];
            vertices[i * 2] = point;
            vertices[i * 2 + 1] = new Vector3(point.x, screenBottom, point.z);
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
