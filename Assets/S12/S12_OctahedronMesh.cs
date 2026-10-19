using UnityEngine;

// S04 방식으로 직접 만든 팔면체 메시 (윗 피라미드 + 아랫 피라미드, 정점 6개, 삼각형 8개)
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S12_OctahedronMesh : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),        // 0
            new Vector3(1f, 0f, 0f),        // 1
            new Vector3(1f, 0f, 1f),        // 2
            new Vector3(0f, 0f, 1f),        // 3
            new Vector3(0.5f, 0.5f, 0.5f),  // 4 위
            new Vector3(0.5f, -0.5f, 0.5f)  // 5 아래
        };

        int[] triangles = new int[]
        {
            1, 0, 4,
            2, 1, 4,
            3, 2, 4,
            0, 3, 4,

            0, 1, 5,
            1, 2, 5,
            2, 3, 5,
            3, 0, 5
        };

        Mesh mesh = new Mesh();
        mesh.name = "S12_Octahedron";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        Material m = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        m.SetColor("_BaseColor", new Color(0.3f, 0.8f, 0.9f));
        GetComponent<MeshRenderer>().sharedMaterial = m;
    }
}
