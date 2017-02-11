using UnityEngine;
using System.Collections;

public class AICustomController : MonoBehaviour
{
    public float moveSpeed = 30f;

    public bool scalePathToSpeed = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private BezierCurve toFollow;

    private float percent = 0f;

    private float followSpeed;

    /// <summary>
    /// Sets the path for the plane to follow as curve
    /// </summary>
    /// <param name="curve"></param>
    public void SetPath(BezierCurve curve)
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        percent = 0f;
        toFollow = curve;
        if(scalePathToSpeed) toFollow.transform.localScale = Vector3.one*moveSpeed;
        toFollow.SetDirty(); //mark as dirty so that length is recalculated
        followSpeed = moveSpeed/toFollow.length;
    }

    public bool FollowingPath { get { return toFollow != null; } }

    void Update()
    {
        if (toFollow == null)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            return;
        }

        percent += followSpeed*Time.deltaTime;

        Vector3 newPoint = startPosition + startRotation * toFollow.GetPointAt(percent);
        transform.rotation = startRotation * toFollow.GetRotationAt(percent);
        transform.position = newPoint;

        if (percent >= 1f)
        {
            toFollow = null;
        }
    }
}
