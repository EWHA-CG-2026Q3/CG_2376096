using UnityEngine;

// S03 방식으로 직접 만든 오각형 메시 (정점 5개, 삼각형 3개)
// 카메라가 -z 쪽에서 볼 때 앞면이 되도록 삼각형 순서를 시계 방향으로 둠
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S12_PentagonMesh : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0.3f, 0f, 0f),    // 0
            new Vector3(0.6f, 0f, 0f),    // 1
            new Vector3(1f, 0.6f, 0f),    // 2
            new Vector3(0.5f, 1f, 0f),    // 3
            new Vector3(0f, 0.6f, 0f)     // 4
        };

        int[] triangles = new int[]
        {
            0, 2, 1,
            0, 4, 2,
            2, 4, 3
        };

        Mesh mesh = new Mesh();
        mesh.name = "S12_Pentagon";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        Material m = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        m.SetColor("_BaseColor", new Color(1f, 0.6f, 0.2f));
        GetComponent<MeshRenderer>().sharedMaterial = m;
    }
}
