using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

[RequireComponent(typeof(CustomCharacterController))]
public class PlayerCustomInput : MonoBehaviour {

    private CustomCharacterController controller;
    private Rewired.Player player;

    [SerializeField]
    private Transform aimSpace;
    private Transform previousAimSpace;

    [SerializeField]
    private Vector3 rotationFactors = Vector3.one;

    [SerializeField]
    private Vector3 driftingRotationFactors = Vector3.one;

    public float engineAwakeTime = 2f;

    private float thrust = 0;

    [SerializeField]
    private float maxThrust = 30;

    [SerializeField]
    private Gun guns;

    [SerializeField]
    private TargetingSystem targetingSystem;

    private Engine engines;

    private SlowMoManager manager;

    void Awake()
    {
        controller = GetComponent<CustomCharacterController>();
        if (aimSpace == null) aimSpace = transform;
        player = Rewired.ReInput.players.GetPlayer(0);
        manager = FindObjectOfType<SlowMoManager>();

        engines = GetComponentInChildren<Engine>();
        engines.OnEngineWake.AddListener(OnStopDrift);
        engines.OnEngineStop.AddListener(OnStartDrift);
    }

	// Update is called once per frame
	void Update () {
        thrust = maxThrust;
        thrust = Mathf.Clamp(thrust, 0, maxThrust);
        if (engines.Running)
        {
            Vector3 movementInput = new Vector3(0, 0, thrust);
            movementInput = aimSpace.TransformVector(movementInput);
            controller.Pushing = movementInput.sqrMagnitude > 0.25f * 0.25f;
            controller.AddForceRelative(movementInput.normalized);
        }

        Vector3 rotationInput = new Vector3(
            player.GetAxis("Pitch"),
            player.GetAxis("Yaw"),
            player.GetAxis("Roll")
        );

        controller.Spinning = rotationInput.sqrMagnitude > 0.25f * 0.25f;
        controller.AddTorqueRelative(Vector3.Scale(rotationInput, controller.Drifting?driftingRotationFactors:rotationFactors));

        if(player.GetButtonDown("Slow Mo") && engines.Running)
        {
            engines.TurnOff();
        }
	    if (player.GetButtonDown("Start Engine") && !engines.Running)
	    {
	        engines.TurnOn();
	    }

        if (player.GetButton("Fire"))
	    {
	        Missile m = guns.Fire();
	        if (m != null)
	        {
	            m.SetTarget(targetingSystem.GetLockedTarget());
	        }
	    }
    }

    public void OnStartDrift()
    {
        controller.Drifting = true;
        controller.useGravity = true;
    }

    public void OnStopDrift()
    {
        controller.Drifting = false;
        controller.useGravity = false;
    }
}
