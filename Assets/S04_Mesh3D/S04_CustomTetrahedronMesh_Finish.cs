using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S04_CustomTetrahedronMesh_Finish : MonoBehaviour
{
    void Start()
    {
        // 정육면체 8개 정점(S3) 중 0, 2, 5, 7번 — 대각선으로 고르면 정사면체가 됨
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(1f, 1f, 1f), // 0 (정육면체 6번)
            new Vector3(1f, 1f, 0f), // 1 (정육면체 2번)
            new Vector3(0f, 1f, 0f), // 2 (정육면체 3번)
            new Vector3(0f, 1f, 1f), // 3 (정육면체 7번)
            new Vector3(0.5f, 0.5f, 0.5f), // 4 (정육면체 가운데 새로운 정점)
        };

        int[] triangles = new int[]
        {
            3, 1, 2,  // 밑면
            3, 0, 1,  // 밑면
            3, 2, 4,  // 옆면 A 이게 문제인 듯
            2, 4, 0,  // 옆면 B 맞음
            0, 4, 1,  // 옆면 C 맞음
            4, 0, 3,  // 옆면 D 맞은
        };

        // 잘못된 예시 — 모든 삼각형의 정점 순서가 반대로 뒤집혀 있음
        //int[] triangles = new int[]
        //{
        //    1, 2, 3,  // 밑면 (원래: 1, 3, 2)
        //    0, 2, 1,  // 옆면 A (원래: 0, 1, 2)
        //    0, 3, 2,  // 옆면 B (원래: 0, 2, 3)
        //    0, 1, 3,  // 옆면 C (원래: 0, 3, 1)
        //};

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
    }
}
