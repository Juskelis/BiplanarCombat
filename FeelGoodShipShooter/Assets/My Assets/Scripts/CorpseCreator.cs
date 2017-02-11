using UnityEngine;
using System.Collections.Generic;

public class CorpseCreator : MonoBehaviour
{
    public Transform corpse;

    public List<Transform> detachOnDeath;

    public void Death()
    {
        Instantiate(corpse, transform.position, transform.rotation);
        foreach(Transform obj in detachOnDeath)
        {
            obj.SetParent(null);
        }
        Destroy(gameObject);
    }
}
