using UnityEngine;

// 직접 만든 사각뿔 메시 (정점 5개, 삼각형 6개). 0/1 좌표계라 기준점(Pivot)이 바닥 모서리에 있음
// 재질은 코드로 덮어쓰지 않음: Mesh Renderer의 Materials에 URP Lit 재질을 직접 끌어다 놓을 것
// 삼각형은 바깥에서 볼 때 시계 방향(Unity의 앞면)
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S13_PyramidMesh : MonoBehaviour
{
    Mesh mesh;

    void OnEnable()
    {
        Vector3[] vertices =
        {
            new Vector3(0f,   0f, 0f),     // 0 바닥 왼쪽 앞
            new Vector3(1f,   0f, 0f),     // 1 바닥 오른쪽 앞
            new Vector3(1f,   0f, 1f),     // 2 바닥 오른쪽 뒤
            new Vector3(0f,   0f, 1f),     // 3 바닥 왼쪽 뒤
            new Vector3(0.5f, 1f, 0.5f)    // 4 꼭대기
        };
        int[] triangles =
        {
            0, 1, 2,   0, 2, 3,   // 바닥
            0, 4, 1,              // 앞면
            1, 4, 2,              // 오른쪽면
            2, 4, 3,              // 뒷면
            3, 4, 0               // 왼쪽면
        };
        Build(vertices, triangles, "S13_Pyramid");
    }

    void Build(Vector3[] v, int[] t, string meshName)
    {
        mesh = new Mesh { name = meshName, hideFlags = HideFlags.HideAndDontSave };
        mesh.vertices = v;
        mesh.triangles = t;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void OnDisable()
    {
        if (mesh != null) DestroyImmediate(mesh);
    }
}
