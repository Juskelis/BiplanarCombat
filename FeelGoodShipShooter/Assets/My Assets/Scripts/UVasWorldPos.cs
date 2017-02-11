using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class UVasWorldPos : MonoBehaviour
{
    private new Renderer renderer;

	// Use this for initialization
	void Start ()
	{
	    renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
	    renderer.material.mainTextureOffset = new Vector2(-transform.position.x, transform.position.z);
	}
}
