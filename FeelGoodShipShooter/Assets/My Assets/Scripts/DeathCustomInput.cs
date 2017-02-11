using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DeathCustomInput : MonoBehaviour
{
    public float startSpeed = 30f;
    public float rotationSpeed = 1f;
    Rigidbody body;
    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Start()
    {
        body.velocity = transform.forward * startSpeed;
        body.angularVelocity = transform.forward * startSpeed;
    }

    void Update()
    {
        transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(body.velocity.normalized, transform.up);
    }
}
