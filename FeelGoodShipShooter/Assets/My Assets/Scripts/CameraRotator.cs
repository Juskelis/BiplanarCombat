using UnityEngine;
using System.Collections;

public class CameraRotator : MonoBehaviour {
    public Vector2 rotationRange = new Vector2(720, 720);
    public float rotationSpeed = 10;
    public float dampingTime = 0.2f;
    public float returnDampingTime = 0.1f;

    private Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Quaternion originalRotation;

    private Rewired.Player player;

    void Start()
    {
        originalRotation = transform.rotation;
        player = Rewired.ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        transform.localRotation = originalRotation;

        float inputH = player.GetAxis("Rotate Horizontal");
        float inputV = player.GetAxis("Rotate Vertical");
        
        if(targetAngles.y > 180)
        {
            targetAngles.y -= 360;
            followAngles.y -= 360;
        }
        if(targetAngles.x > 180)
        {
            targetAngles.x -= 360;
            followAngles.x -= 360;
        }
        if(targetAngles.y < -180)
        {
            targetAngles.y += 360;
            followAngles.y += 360;
        }
        if(targetAngles.x < -180)
        {
            targetAngles.x += 360;
            followAngles.y += 360;
        }

        bool returning = Mathf.Abs(inputH) > 0.25f || Mathf.Abs(inputV) > 0.25f;

        if (returning)
        {
            targetAngles.y += inputH * rotationSpeed;
            targetAngles.x += inputV * rotationSpeed;
        }
        else
        {
            targetAngles.y = Mathf.MoveTowardsAngle(targetAngles.y, originalRotation.eulerAngles.y, rotationSpeed);
            targetAngles.x = Mathf.MoveTowardsAngle(targetAngles.x, originalRotation.eulerAngles.x, rotationSpeed);
        }

        targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
        targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);

        followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);

        transform.localRotation = originalRotation * Quaternion.Euler(-followAngles.x, followAngles.y, 0);
    }
}
