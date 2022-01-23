using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public float maxZ = 19.5f;
    public float minZ = -19.5f;
    public float maxX = 19.5f;
    public float minX = -19.5f;
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        transform.position = new Vector3(x, 0, z);
    }
}
