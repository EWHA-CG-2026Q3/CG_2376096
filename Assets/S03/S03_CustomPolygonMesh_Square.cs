  using UnityEngine;

  [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
  public class S03_CustomPolygonMesh_Square : MonoBehaviour
  {
      void Start()
      {
          Vector3[] vertices = new Vector3[]
          {
            new Vector3(0.3f, 0f, 0f),    // 0
            new Vector3(0.6f, 0f, 0f),    // 1
            new Vector3(1f, 0.6f, 0f),    // 2
            new Vector3(0.5f, 1f, 0f)     // 3
            new Vector3(0f, 0.6f, 0f)     // 4
          };

          // TODO 2: 정점 3개씩 묶어 삼각형들을 구성하세요
          int[] triangles = new int[]
          {
              // 예: 0, 1, 2,
          };

          Mesh mesh = new Mesh();
          mesh.vertices = vertices;
          mesh.triangles = triangles;
          mesh.RecalculateNormals();

          GetComponent<MeshFilter>().mesh = mesh;
          GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
      }
  }