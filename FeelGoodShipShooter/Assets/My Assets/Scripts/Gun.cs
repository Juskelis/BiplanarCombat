using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Gun : MonoBehaviour {

    private CustomCharacterController controller;

    public Missile bullet;

    public ShakeSettings shakeSettings;

    public UnityEvent onFire;

    public float fireRate = 1f;
    private float timeSinceLastFire;

    Rewired.Player player;

    void Awake()
    {
        controller = GetComponentInParent<CustomCharacterController>();
        timeSinceLastFire = fireRate * 2f;
        player = Rewired.ReInput.players.GetPlayer(0);
    }

    public void Update()
    {
        timeSinceLastFire += Time.deltaTime;
    }

    public Missile Fire()
    {
        if (timeSinceLastFire < fireRate) return null;

        timeSinceLastFire = 0f;
        Missile copy = Instantiate(bullet);
        copy.transform.position = transform.position;
        copy.transform.rotation = transform.rotation;
        shakeSettings.Shake();
        onFire.Invoke();
        return copy;
    }
}
