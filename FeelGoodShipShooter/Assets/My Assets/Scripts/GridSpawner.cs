using TypeReferences;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridSpawner : MonoBehaviour {

    public Transform toSpawn;

    public int numberToSpawn = 100;

    public Vector3 range;

    public float maxScale = 1f;

    public bool relative = false;

    public bool maintainCount = false;

    [ClassExtends(typeof(MonoBehaviour))]
    public ClassTypeReference typeToSearch;

	// Use this for initialization
	void Start () {
	    for(int i = 0; i < numberToSpawn; i++)
        {
            SpawnOne();
        }
	}

    void SpawnOne()
    {
        Transform clone = Instantiate<Transform>(toSpawn);
        Vector3 pos = new Vector3(
            (int)(Random.Range(-1.0f, 1) * range.x),
            (int)(Random.Range(-1.0f, 1) * range.y),
            (int)(Random.Range(-1.0f, 1) * range.z)
        );
        if (relative) pos += transform.position;
        Vector3 scale = Vector3.one * ((int)Random.Range(1, maxScale));
        clone.position = pos;
        clone.localScale = scale;
        clone.Rotate(Vector3.up * Random.Range(0, 4) * 90f);
        clone.SetParent(transform);
    }

    void Update()
    {
        if (maintainCount)
        {
            int count = FindObjectsOfType(typeToSearch.Type).Length;
            for (; count < numberToSpawn; count++)
            {
                SpawnOne();
            }
        }
    }
}
