using UnityEngine;

public class TargetingSystem : MonoBehaviour {
    [SerializeField]
    private float range = 100f;

    [SerializeField]
    private float lockTime = 5f;

    [SerializeField]
    private float coneAngle = 0.25f;

    [SerializeField]
    private LayerMask targetingLayers;

    private CustomCharacterController target;
    private float startedTargetingTime = float.PositiveInfinity;

    public CustomCharacterController GetLockedTarget()
    {
        return target != null && LockedOn ? target : null;
    }

    public CustomCharacterController GetTarget()
    {
        return target;
    }

    public bool LockedOn { get { return Time.time - startedTargetingTime > lockTime; } }

    void Start()
    {
        target = null;
    }

    void Update()
    {
        if (target == null)
        {
            target = FindTarget();
            if (target != null)
            {
                //found target
                startedTargetingTime = Time.time;
            }
            else
            {
                //short circuit
                return;
            }
        }

        //at this point, target is not null
        Vector3 distanceToTarget = target.transform.position - transform.position;
        float angleFromForward = Mathf.Abs(Vector3.Angle(distanceToTarget.normalized, transform.forward));
        if (distanceToTarget.magnitude > range || angleFromForward > coneAngle)
        {
            target = null;
        }
    }

    /// <summary>
    /// Finds a target in range
    /// </summary>
    /// <returns>Valid target or null if it can't find one</returns>
    private CustomCharacterController FindTarget()
    {
        foreach (Collider c in Physics.OverlapSphere(transform.position, range, targetingLayers))
        {
            if(c.transform == transform) continue;

            Vector3 direction = (c.transform.position - transform.position).normalized;
            if (Mathf.Abs(Vector3.Angle(direction, transform.forward)) <= coneAngle
                && c.GetComponent<CustomCharacterController>() != null)
            {
                return c.GetComponent<CustomCharacterController>();
            }
        }
        return null;
    }
}
