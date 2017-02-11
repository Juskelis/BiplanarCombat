using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public LayerMask collisionLayers;
    public int possibleHits = 1;

    public UnityEvent onDeath;

    private bool dead = false;

    void OnCollisionEnter(Collision c)
    {
        if (((1 << c.gameObject.layer) & collisionLayers.value) == 0) return;
        possibleHits -= 1;
        if (possibleHits <= 0 && !dead)
        {
            onDeath.Invoke();
            dead = true;
        }
    }

}
