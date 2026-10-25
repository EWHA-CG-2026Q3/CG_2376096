using UnityEngine;

// 직접 만든 팔면체 메시 (윗 피라미드 + 아랫 피라미드, 정점 6개, 삼각형 8개)
// 재질은 코드로 덮어쓰지 않음: Mesh Renderer의 Materials에 URP Lit 재질을 직접 끌어다 놓을 것
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S13_OctahedronMesh : MonoBehaviour
{
    Mesh mesh;

    void OnEnable()
    {
        Vector3[] vertices =
        {
            new Vector3(0f, 0f, 0f),        // 0
            new Vector3(1f, 0f, 0f),        // 1
            new Vector3(1f, 0f, 1f),        // 2
            new Vector3(0f, 0f, 1f),        // 3
            new Vector3(0.5f, 0.5f, 0.5f),  // 4 위
            new Vector3(0.5f, -0.5f, 0.5f)  // 5 아래
        };
        int[] triangles =
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
        mesh = new Mesh { name = "S13_Octahedron", hideFlags = HideFlags.HideAndDontSave };
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void OnDisable()
    {
        if (mesh != null) DestroyImmediate(mesh);
    }
}
