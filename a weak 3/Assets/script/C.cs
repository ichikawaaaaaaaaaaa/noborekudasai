using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;       // プレイヤーのTransform
    public Vector3 offset = new Vector3(0, 5, -7); // カメラの相対位置
    public float smoothSpeed = 5f; // 追従のなめらかさ

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

     
    }
}
