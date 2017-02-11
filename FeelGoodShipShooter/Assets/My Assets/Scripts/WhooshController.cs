using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class WhooshController : MonoBehaviour {
    public CustomCharacterController speedInput;

    private ParticleSystem system;

    void Awake()
    {
        system = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        system.startSpeed = Mathf.Lerp(system.startSpeed, speedInput.WingSpeed.magnitude, 0.5f);
    }
}
