using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    Transform toFollow;

    [SerializeField]
    Vector3 nearOffset;

    [SerializeField]
    Vector3 farOffset;

    [SerializeField]
    Vector3 driftOffset;

    [SerializeField]
    Transform toLookAt;

    [SerializeField]
    [Range(0,1)]
    float rotationBias = 0.96f;
    [SerializeField]
    [Range(0, 1)]
    float offsetBias = 0.96f;

    [SerializeField]
    CustomCharacterController player;

    private Vector3 previousOffset;

    private float playerSpeedPercentage;

    void LateUpdate()
    {
        if (toFollow == null || toLookAt == null) return;
        playerSpeedPercentage = player.WingSpeed.magnitude / player.maxSpeed;
        Vector3 offset = Vector3.Lerp(nearOffset, farOffset, playerSpeedPercentage);
        if (player.Drifting) offset = driftOffset;
        offset = previousOffset * offsetBias + offset * (1 - offsetBias);
        previousOffset = offset;

        Vector3 moveTo = toFollow.position
            + toFollow.right * offset.x
            + toFollow.up * offset.y
            + toFollow.forward * offset.z;

        Vector3 currentUp = transform.up * rotationBias + toFollow.up * (1 - rotationBias);
        //currentUp = Vector3.Lerp(currentUp, Vector3.up, 0.5f);

        transform.position = moveTo;
        transform.LookAt(toLookAt, currentUp);
    }
}
