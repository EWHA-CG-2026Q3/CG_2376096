using UnityEngine;

// 직접 만든 사면체 메시 (정점 4개, 삼각형 4개). 기준점(Pivot)이 바닥에 있음
// 재질은 코드로 덮어쓰지 않음: Mesh Renderer의 Materials에 URP Lit 재질을 직접 끌어다 놓을 것
// 삼각형은 바깥에서 볼 때 시계 방향(Unity의 앞면)
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S13_TetrahedronMesh : MonoBehaviour
{
    Mesh mesh;

    void OnEnable()
    {
        Vector3[] vertices =
        {
            new Vector3(0f,   0f, 0f),     // 0 A 바닥
            new Vector3(1f,   0f, 0f),     // 1 B 바닥
            new Vector3(0.5f, 0f, 1f),     // 2 C 바닥
            new Vector3(0.5f, 1f, 0.4f)    // 3 D 꼭대기
        };
        int[] triangles =
        {
            0, 2, 1,   // 바닥 (아래를 향함)
            0, 3, 1,   // A-B 면
            1, 3, 2,   // B-C 면
            2, 3, 0    // C-A 면
        };
        mesh = new Mesh { name = "S13_Tetrahedron", hideFlags = HideFlags.HideAndDontSave };
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
