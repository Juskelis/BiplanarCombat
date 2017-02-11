using UnityEngine;
using System.Collections;

public class SlowMoManager : MonoBehaviour {
    [SerializeField]
    private float slowAmount = 0.5f;

    [SerializeField]
    private float slowTime = 1f;

    [SerializeField]
    private float switchTime = 1f;

    private bool slowed = false;
    public bool Slowed { get { return slowed; } }

    private float moveTime = 0f;

    void Update()
    {
        if(!slowed)
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, 1f/switchTime * Time.unscaledDeltaTime);
    }

    public void Slow(bool autoTimed = true)
    {
        slowed = true;
        if(autoTimed) Invoke("StopSlow", slowTime);
        Time.timeScale = slowAmount;
    }

    public void StopSlow()
    {
        slowed = false;
    }
}
