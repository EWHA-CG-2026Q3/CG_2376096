using UnityEngine;

// 무빙헤드 조명 (중간 프로젝트 ④ 변환)
// 계층: Truss > Yoke(좌우 회전, pan) > Body(위아래 회전, tilt) > 조명 캔, 렌즈, 빔, Spot Light
// 각 단계의 움직임은 우리 행렬 T × R 로 계산함
public class LH_MovingHead : MonoBehaviour
{
    public Transform body;               // Yoke의 자식
    public float panRange = 60f;         // 좌우로 흔드는 각도
    public float panSpeed = 0.5f;
    public float tiltRange = 35f;        // 위아래로 흔드는 각도
    public float tiltSpeed = 0.8f;
    public float tiltOffset = 10f;

    Vector3 yokePos, bodyPos;

    void Start()
    {
        yokePos = transform.localPosition;
        if (body != null) bodyPos = body.localPosition;
    }

    void Update()
    {
        float t = Time.time;
        float pan = panRange * Mathf.Sin(t * panSpeed);
        float tilt = tiltOffset + tiltRange * Mathf.Sin(t * tiltSpeed);

        // Yoke: 제자리(T)에서 y축으로 회전(R)
        LH_Matrix.Apply(transform, LH_Matrix.Translate(yokePos) * LH_Matrix.RotateY(pan));
        // Body: Yoke 아래 위치(T)에서 x축으로 회전(R) → 부모의 회전과 합쳐짐
        if (body != null)
            LH_Matrix.Apply(body, LH_Matrix.Translate(bodyPos) * LH_Matrix.RotateX(tilt));
    }
}
