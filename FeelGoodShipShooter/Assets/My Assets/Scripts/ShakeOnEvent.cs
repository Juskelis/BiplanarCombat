using UnityEngine;
using System.Collections;

public class ShakeOnEvent : MonoBehaviour {
    public ShakeSettings settings;

    public void Shake()
    {
        settings.Shake();
    }
}
