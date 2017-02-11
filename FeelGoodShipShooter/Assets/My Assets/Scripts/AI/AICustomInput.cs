using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AICustomController))]
public class AICustomInput : MonoBehaviour
{
    public Transform target;

    public BezierCurve curve;

    private AICustomController controller;

    void Awake()
    {
        controller = GetComponent<AICustomController>();
    }
	
	void Update ()
	{
	    //if (target == null && !FindTarget()) return;

	    if (!controller.FollowingPath)
	    {
	        controller.SetPath(curve);
	    }
	}

    //returns if target could be found
    bool FindTarget()
    {
        target = FindObjectOfType<PlayerCustomInput>().transform;
        return target == null;
    }
}
