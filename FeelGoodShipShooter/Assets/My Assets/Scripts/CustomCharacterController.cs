using UnityEngine;
using System.Collections;

public class CustomCharacterController : MonoBehaviour {
    public Vector3 startSpeed;

    public float forceFactor = 1f;
    public float torqueFactor = 1f;

    public float movingFriction = 5f;
    public float stoppingFriction = 1f;
    public float driftingFriction = 1f;

    public float movingTorqueFriction = 5f;
    public float stoppingTorqueFriction = 1f;

    public float maxSpeed = 10f;
    public float maxTorque = 10f;

    public bool useGravity = false;
    public float gravitySpeed = 10f;
    public float driftingGravitySpeed = 10f;
    public float minGravityMultiplier = 1f;
    public float maxGravityMultiplier = 10f;

    private Vector3 speed;
    public Vector3 Speed { get { return speed + externalForces; } }
    public Vector3 WingSpeed {
        get {
            if (Drifting || Vector3.Dot(Speed, transform.forward) <= 0) return Vector3.zero;
            return Vector3.Project(Speed, transform.forward);
        }
    }
    private Vector3 rotationSpeed;
    public Vector3 RotationSpeed { get { return rotationSpeed; } }

    public bool Pushing
    {
        get; set;
    }

    public bool Spinning
    {
        get; set;
    }

    public bool Drifting
    {
        get; set;
    }

    private Vector3 externalForces = Vector3.zero;

    public void Start()
    {
        speed = startSpeed;
    }

    public void Update()
    {
        transform.position += Speed * Time.deltaTime;
        transform.Rotate(rotationSpeed * Time.deltaTime);
        float friction = Drifting ? driftingFriction : (Pushing ? movingFriction : stoppingFriction);
        speed = Vector3.MoveTowards(speed, Vector3.zero, friction);
        rotationSpeed = Vector3.MoveTowards(
            rotationSpeed,
            Vector3.zero,
            Spinning ? movingTorqueFriction : stoppingTorqueFriction
        );

        externalForces = Vector3.MoveTowards(
            externalForces,
            Physics.gravity.normalized * (useGravity?maxGravityMultiplier:minGravityMultiplier),
            (Drifting?driftingGravitySpeed:gravitySpeed)*Time.deltaTime);
        externalForces = Vector3.ClampMagnitude(externalForces, maxSpeed);
    }

    public void AddForceRelative(Vector3 normalizedForce)
    {
        speed += normalizedForce * forceFactor * Time.deltaTime;
        speed = Vector3.ClampMagnitude(speed, maxSpeed);
    }

    public void AddTorqueRelative(Vector3 normalizedTorque)
    {
        rotationSpeed += normalizedTorque * torqueFactor;
        rotationSpeed = Vector3.ClampMagnitude(rotationSpeed, maxTorque);
    }

    public void SetTorque(Vector3 normalizedTorque)
    {
        rotationSpeed += normalizedTorque * torqueFactor;
        rotationSpeed = Vector3.ClampMagnitude(rotationSpeed, maxTorque);
    }
}
