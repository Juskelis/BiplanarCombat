using UnityEngine;
using System.Collections;

public class FlightPath
{
    public BezierCurve curve;

    public float radius;

    /// <summary>
    /// Traces a sphere collider along the path, and returns whether or not any collisions happened
    /// </summary>
    /// <param name="curve">The <see cref="BezierCurve"/> to follow</param>
    /// <param name="radius">The radius of the sphere collider that traces the curve</param>
    /// <param name="startPosition">Where the curve should start</param>
    /// <param name="startRotation">What rotation should be applied to the curve</param>
    /// <returns></returns>
    public bool CanTravelPath(BezierCurve curve, float radius,
        Vector3 startPosition = default(Vector3), Quaternion startRotation = default(Quaternion))
    {
        return false;
    }
}
