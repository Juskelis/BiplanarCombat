using UnityEngine;
using System.Collections;

public class LandGenerator : MonoBehaviour
{
    public Transform chunk;
    public int width;
    public int depth;
    public int maxHeight;

    public float cubeSize = 1f;

    public float cubeCellSize = 1f;

    void Start()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        Vector3 offset = new Vector3(
            -(width*cubeSize)/2f + cubeSize/2f,
            0,
            -(depth*cubeSize)/2f + cubeSize/2f);

        float maxDistance = (Mathf.Min(width, depth)*cubeSize)/2f;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                Transform clone = Instantiate(chunk);
                clone.position = new Vector3(x*cubeSize,0,z*cubeSize) + offset;
                clone.localScale = new Vector3(
                    cubeSize,
                    Random.Range(0.01f, maxHeight) * Mathf.Clamp01(1f - clone.position.magnitude/maxDistance),
                    cubeSize);
                clone.position += Vector3.up*clone.localScale.y/3f;
                clone.SetParent(transform, false);

                if (clone.localScale.y < cubeCellSize)
                {
                    Destroy(clone.gameObject);
                }
            }
        }
    }
}
