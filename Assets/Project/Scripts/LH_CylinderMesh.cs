using UnityEngine;

// 반복문으로 만드는 원기둥/원뿔/원뿔대 메시 (중간 프로젝트 ① 기하 모델링)
// - 원 위의 점 i의 각도 = 360° / segments * i
// - 아래 원(반지름 radiusBottom, y = 0)과 위 원(반지름 radiusTop, y = height)을 옆면 사각형(삼각형 2개)으로 이음
// - radiusTop = 0 이면 원뿔, 두 반지름이 같으면 원기둥, segments = 6 이면 육각기둥
// 기준점(pivot): 아래 원의 중심. 메시는 +y 방향으로 자람
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class LH_CylinderMesh : MonoBehaviour
{
    [Range(3, 64)] public int segments = 24;
    public float radiusBottom = 0.5f;
    public float radiusTop = 0.5f;
    public float height = 1f;
    public bool capBottom = true;
    public bool capTop = true;

    Mesh mesh;
    bool dirty = true;

    void OnEnable() { dirty = true; Build(); }
    void OnValidate() { dirty = true; }          // Inspector에서 값을 바꾸면 다음 Update에서 다시 만듦
    void Update() { if (dirty) Build(); }

    void Build()
    {
        dirty = false;
        if (mesh == null)
        {
            mesh = new Mesh { name = "LH_Cylinder", hideFlags = HideFlags.HideAndDontSave };
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }
        mesh.Clear();

        int n = Mathf.Max(3, segments);
        var v = new System.Collections.Generic.List<Vector3>();
        var uv = new System.Collections.Generic.List<Vector2>();
        var tri = new System.Collections.Generic.List<int>();

        // 옆면: 아래 점과 위 점을 n + 1개씩 (마지막 점은 첫 점과 같은 위치, 텍스처 이음매용)
        for (int i = 0; i <= n; i++)
        {
            float a = 360f / n * i * Mathf.Deg2Rad;
            float c = Mathf.Cos(a), s = Mathf.Sin(a);
            v.Add(new Vector3(c * radiusBottom, 0f, s * radiusBottom));   // 아래: 2i
            v.Add(new Vector3(c * radiusTop, height, s * radiusTop));     // 위:   2i + 1
            uv.Add(new Vector2((float)i / n, 0f));
            uv.Add(new Vector2((float)i / n, 1f));
        }
        for (int i = 0; i < n; i++)
        {
            int b0 = 2 * i, t0 = 2 * i + 1, b1 = 2 * (i + 1), t1 = 2 * (i + 1) + 1;
            // 바깥에서 볼 때 시계 방향 (Unity의 앞면)
            tri.Add(b0); tri.Add(t0); tri.Add(b1);
            tri.Add(b1); tri.Add(t0); tri.Add(t1);
        }

        if (capBottom && radiusBottom > 0f) AddCap(v, uv, tri, n, 0f, radiusBottom, false);
        if (capTop && radiusTop > 0f) AddCap(v, uv, tri, n, height, radiusTop, true);

        mesh.SetVertices(v);
        mesh.SetUVs(0, uv);
        mesh.SetTriangles(tri, 0);
        var col = new Color[v.Count];
        for (int i = 0; i < col.Length; i++) col[i] = Color.white;   // 파티클 셰이더(빔)용 정점 색
        mesh.colors = col;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    // 뚜껑: 가운데 점 + 원 위의 이웃한 두 점(i, (i + 1) % n)
    void AddCap(System.Collections.Generic.List<Vector3> v, System.Collections.Generic.List<Vector2> uv,
                System.Collections.Generic.List<int> tri, int n, float y, float r, bool top)
    {
        int center = v.Count;
        v.Add(new Vector3(0f, y, 0f));
        uv.Add(new Vector2(0.5f, 0.5f));
        for (int i = 0; i < n; i++)
        {
            float a = 360f / n * i * Mathf.Deg2Rad;
            v.Add(new Vector3(Mathf.Cos(a) * r, y, Mathf.Sin(a) * r));
            uv.Add(new Vector2(0.5f + 0.5f * Mathf.Cos(a), 0.5f + 0.5f * Mathf.Sin(a)));
        }
        for (int i = 0; i < n; i++)
        {
            int p0 = center + 1 + i;
            int p1 = center + 1 + (i + 1) % n;
            if (top) { tri.Add(center); tri.Add(p1); tri.Add(p0); }   // 위에서 볼 때 앞면
            else     { tri.Add(center); tri.Add(p0); tri.Add(p1); }   // 아래에서 볼 때 앞면
        }
    }

    void OnDisable()
    {
        if (mesh != null) DestroyImmediate(mesh);
        mesh = null;
    }
}
