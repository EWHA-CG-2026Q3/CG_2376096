using UnityEngine;

// 우리 코드로 만드는 행렬 (S08–S12에서 직접 만든 것과 같은 방식)
public static class LH_Matrix
{
    public static Matrix4x4 Translate(Vector3 t)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.m03 = t.x; m.m13 = t.y; m.m23 = t.z;
        return m;
    }

    public static Matrix4x4 Scale(Vector3 s)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.m00 = s.x; m.m11 = s.y; m.m22 = s.z;
        return m;
    }

    public static Matrix4x4 RotateX(float deg)
    {
        float r = deg * Mathf.Deg2Rad, c = Mathf.Cos(r), s = Mathf.Sin(r);
        Matrix4x4 m = Matrix4x4.identity;
        m.m11 = c; m.m12 = -s;
        m.m21 = s; m.m22 = c;
        return m;
    }

    public static Matrix4x4 RotateY(float deg)
    {
        float r = deg * Mathf.Deg2Rad, c = Mathf.Cos(r), s = Mathf.Sin(r);
        Matrix4x4 m = Matrix4x4.identity;
        m.m00 = c;  m.m02 = s;
        m.m20 = -s; m.m22 = c;
        return m;
    }

    // 행렬을 Transform의 로컬 위치, 회전, 크기로 옮김
    public static void Apply(Transform t, Matrix4x4 m)
    {
        Vector3 x = m.GetColumn(0), y = m.GetColumn(1), z = m.GetColumn(2);
        t.localPosition = m.GetColumn(3);
        t.localScale = new Vector3(x.magnitude, y.magnitude, z.magnitude);
        t.localRotation = Quaternion.LookRotation(z / z.magnitude, y / y.magnitude);
    }
}
