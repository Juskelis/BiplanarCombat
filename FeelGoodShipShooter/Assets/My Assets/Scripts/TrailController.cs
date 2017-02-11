using UnityEngine;
using System.Collections;

public class TrailController : MonoBehaviour {
    public CustomCharacterController controller;

    public float maxTime = 1f;

    public float springBias = 0.96f;

    private TrailRenderer[] renderers;

    void Awake()
    {
        renderers = GetComponentsInChildren<TrailRenderer>();
    }

    void Update()
    {
        foreach(TrailRenderer r in renderers)
        {
            float target = maxTime * (controller.WingSpeed.magnitude / controller.maxSpeed);
            r.time = r.time * springBias + target * (1 - springBias);
        }
    }
}
