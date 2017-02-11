using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Engine : MonoBehaviour {
    [SerializeField]
    private ParticleSystem enginePuff;

    [SerializeField]
    private int minStarts = 3;

    [SerializeField]
    private int maxStarts = 12;

    private int startsNeeded;
    private int currentStarts = 0;


    public UnityEvent OnEngineStop;
    public UnityEvent OnEngineStart;
    public UnityEvent OnEngineWake;

    private bool on = true;
    private bool On { get { return on; } }

    private bool running = true;
    public bool Running { get { return running; } }

    private Rewired.Player player;

    private PlayerCustomInput input;

    void Awake()
    {
        player = Rewired.ReInput.players.GetPlayer(0);
        input = GetComponentInParent<PlayerCustomInput>();
        enginePuff.Stop();
    }

    public void TurnOff()
    {
        on = false;
        running = false;

        currentStarts = 0;
        startsNeeded = Random.Range(minStarts, maxStarts);

        OnEngineStop.Invoke();
    }

    /// <summary>
    /// Flips the switch to activate the engines.
    /// </summary>
    public void TurnOn()
    {
        on = true;
        currentStarts++;
        OnEngineStart.Invoke();
        if (currentStarts > startsNeeded)
        {
            WakeUpEngines();
        }
    }

    void WakeUpEngines()
    {
        running = true;
        OnEngineWake.Invoke();
    }
}
