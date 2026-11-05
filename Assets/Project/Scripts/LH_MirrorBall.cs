using UnityEngine;

// 미러볼: 천천히 돌면서(R) 살짝 흔들리고(T) 박자에 맞춰 커졌다 작아짐(S)
// M = T × R × S (우리 행렬)
public class LH_MirrorBall : MonoBehaviour
{
    public float spinSpeed = 30f;       // 초당 회전 각도
    public float swing = 0.03f;         // 흔들림 크기
    public float pulse = 0.04f;         // 크기 변화 비율
    public float bpm = 120f;

    Vector3 basePos, baseScale;

    void Start()
    {
        basePos = transform.localPosition;
        baseScale = transform.localScale;
    }

    void Update()
    {
        float t = Time.time;
        Vector3 offset = new Vector3(Mathf.Sin(t * 0.7f), 0f, Mathf.Cos(t * 0.9f)) * swing;
        float beat = 1f + pulse * Mathf.Max(0f, Mathf.Sin(t * bpm / 60f * Mathf.PI * 2f));

        Matrix4x4 m = LH_Matrix.Translate(basePos + offset)
                    * LH_Matrix.RotateY(t * spinSpeed)
                    * LH_Matrix.Scale(baseScale * beat);
        LH_Matrix.Apply(transform, m);
    }
}
