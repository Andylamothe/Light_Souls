using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterWave : MonoBehaviour
{
    public float waveHeight = 0.05f;
    public float waveFrequency = 1.5f;
    public float waveSpeed = 1f;

    private Vector3[] baseVertices;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
    }

    void Update()
    {
        Vector3[] vertices = new Vector3[baseVertices.Length];
        float time = Time.time * waveSpeed;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = baseVertices[i];
            v.y += Mathf.Sin(v.x * waveFrequency + time) * waveHeight;
            v.y += Mathf.Cos(v.z * waveFrequency + time) * waveHeight;
            vertices[i] = v;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
