using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ExplodeOnContact : MonoBehaviour {
    public Transform explosion;
    public LayerMask collisionLayers;
    public bool explodeFromCenter = false;
    public UnityEvent onExplode;

    public bool timedDestruction = false;
    public float timedDestructionTime = 5f;

    public bool detachParticles = true;

    void Start()
    {
        if(timedDestruction) Invoke("Death", timedDestructionTime);
    }

    void Death()
    {
        onExplode.Invoke();
        Transform clone = Instantiate(explosion);
        clone.position = transform.position;
        clone.rotation = transform.rotation;
        clone.localScale = transform.localScale;
        if (detachParticles)
        {
            foreach (ParticleSystem child in GetComponentsInChildren<ParticleSystem>())
            {
                child.transform.SetParent(null);
            }
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision c)
    {
        if (((1 << c.gameObject.layer) & collisionLayers.value) == 0) return;
        onExplode.Invoke();
        Transform clone = Instantiate(explosion);
        clone.position = explodeFromCenter ? transform.position : c.contacts[0].point;
        clone.rotation = Quaternion.LookRotation(Vector3.forward, c.contacts[0].normal);
        clone.localScale = transform.localScale;
        if (detachParticles)
        {
            foreach (ParticleSystem child in GetComponentsInChildren<ParticleSystem>())
            {
                child.transform.SetParent(null);
            }
        }
        Destroy(gameObject);
    }
}
