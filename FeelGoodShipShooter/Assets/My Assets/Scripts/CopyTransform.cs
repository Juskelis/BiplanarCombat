using UnityEngine;
using System.Collections;

public class CopyTransform : MonoBehaviour {
    public Transform toCopy;
    public CopyV3 posCopy;
    public CopyV3 rotCopy;
    public CopyV3 scaleCopy;

	// Update is called once per frame
	void Update ()
	{
	    if (toCopy == null) return;
        transform.position = posCopy.Apply(transform.position, toCopy.position);
	    transform.rotation = Quaternion.Euler(rotCopy.Apply(transform.rotation.eulerAngles, toCopy.rotation.eulerAngles));
	    transform.localScale = scaleCopy.Apply(transform.localScale, toCopy.localScale);
    }

    [System.Serializable]
    public class CopyV3
    {
        public bool copyX = false;
        public bool copyY = false;
        public bool copyZ = false;

        public Vector3 Apply(Vector3 target, Vector3 toCopy)
        {
            Vector3 copy = target;
            copy.x = copyX ? toCopy.x : target.x;
            copy.y = copyY ? toCopy.y : target.y;
            copy.z = copyZ ? toCopy.z : target.z;
            return copy;
        }
    }
}
