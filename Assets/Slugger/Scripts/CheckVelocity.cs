using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVelocity : MonoBehaviour
{
    GameObject cube;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        cube = this.gameObject;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Velocity" + rb.velocity);
    }
}
