using UnityEngine;

// VirtualCamera를 z축 방향으로 앞뒤로 왕복시킴 (Main Camera는 자식이라 함께 움직임)
// 직접 옮겨 보고 싶으면 이 컴포넌트를 체크 해제하고 Position Z를 바꾸면 됨
public class S12_CameraMover : MonoBehaviour
{
    public float minZ = -8f;     // 가장 뒤쪽 z
    public float maxZ = -2f;     // 가장 앞쪽 z
    public float speed = 1.5f;   // 초당 이동 거리

    void Update()
    {
        Vector3 p = transform.position;
        p.z = minZ + Mathf.PingPong(Time.time * speed, maxZ - minZ);
        transform.position = p;
    }
}
