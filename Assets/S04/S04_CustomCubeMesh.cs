using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S04_CustomCubeMesh : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),        // 0
            new Vector3(1f, 0f, 0f),        // 1
            new Vector3(1f, 0f, 1f),        // 2
            new Vector3(0f, 0f, 1f),        // 3
            new Vector3(0.5f, 0.5f, 0.5f),  // 4
            new Vector3(0.5f, -0.5f, 0.5f)  // 5
        };

        int[] triangles = new int[]
        {
            // 윗 피라미드 (정점 4)
            1, 0, 4,
            2, 1, 4,
            3, 2, 4,
            0, 3, 4,

            // 아랫 피라미드 (정점 5)
            0, 1, 5,
            1, 2, 5,
            2, 3, 5,
            3, 0, 5
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
    }
}