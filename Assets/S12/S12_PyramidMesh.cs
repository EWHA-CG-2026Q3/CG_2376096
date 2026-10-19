using UnityEngine;

// S03/S04 방식으로 직접 만든 사각뿔 메시 (정점 5개, 삼각형 6개)
// 삼각형은 바깥에서 볼 때 시계 방향(Unity의 앞면)이 되도록 순서를 맞춤
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S12_PyramidMesh : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f,   0f, 0f),     // 0 바닥 왼쪽 앞
            new Vector3(1f,   0f, 0f),     // 1 바닥 오른쪽 앞
            new Vector3(1f,   0f, 1f),     // 2 바닥 오른쪽 뒤
            new Vector3(0f,   0f, 1f),     // 3 바닥 왼쪽 뒤
            new Vector3(0.5f, 1f, 0.5f)    // 4 꼭대기
        };

        int[] triangles = new int[]
        {
            0, 1, 2,   0, 2, 3,   // 바닥
            0, 4, 1,              // 앞면
            1, 4, 2,              // 오른쪽면
            2, 4, 3,              // 뒷면
            3, 4, 0               // 왼쪽면
        };

        Mesh mesh = new Mesh();
        mesh.name = "S12_Pyramid";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        Material m = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        m.SetColor("_BaseColor", new Color(0.9f, 0.4f, 0.5f));
        GetComponent<MeshRenderer>().sharedMaterial = m;
    }
}
