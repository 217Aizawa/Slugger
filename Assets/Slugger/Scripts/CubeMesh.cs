using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : MonoBehaviour
{
    private Mesh mesh;
    private MeshCollider meshCollider;
    private MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        mesh.Clear();
    }
}
