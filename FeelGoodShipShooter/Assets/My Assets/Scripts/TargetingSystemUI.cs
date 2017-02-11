using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetingSystemUI : MonoBehaviour {
    public TargetingSystem system;

    public RectTransform targetingIndicator;

    public RectTransform lockedIndicator;

    void Update()
    {
        targetingIndicator.gameObject.SetActive(false);
        lockedIndicator.gameObject.SetActive(false);
        CustomCharacterController target = system.GetTarget();
        if(target != null && Vector3.Dot(target.transform.position - Camera.main.transform.position, Camera.main.transform.forward) > 0)
        {
            RectTransform indicator;
            if(system.LockedOn)
            {
                indicator = lockedIndicator;
            }
            else
            {
                indicator = targetingIndicator;
            }
            indicator.gameObject.SetActive(true);
            indicator.position = Camera.main.WorldToScreenPoint(target.transform.position);
        }
    }
}
