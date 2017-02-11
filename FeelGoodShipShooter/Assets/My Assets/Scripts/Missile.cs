using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public float moveSpeed;

    public float turnSpeed;

    [SerializeField]
    protected CustomCharacterController target;

    void Update()
    {
        Vector3 targetDirection = transform.forward;
        if (target != null)
        {
            float secondsToReach = (target.transform.position - transform.position).magnitude/moveSpeed;
            Vector3 targetPosition = target.transform.position + target.Speed*secondsToReach;
            targetDirection = (targetPosition - transform.position).normalized;
        }

        Quaternion targetQuaternion = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetQuaternion,
            turnSpeed*Time.deltaTime);

        transform.position += transform.forward*moveSpeed*Time.deltaTime;
    }

    public virtual void SetTarget(CustomCharacterController target)
    {
        this.target = target;
    }
}
