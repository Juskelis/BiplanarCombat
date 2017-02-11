using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AltitudeFromTransform : MonoBehaviour {
    public Transform toTrack;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = toTrack.position.y.ToString();
    }
}
