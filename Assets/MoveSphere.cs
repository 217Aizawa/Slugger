using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3000;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 0, speed, ForceMode.Force);
    }

    void Update()
    {
        //時速に変換して表示
        Debug.Log("speed" + rb.velocity.magnitude
            * 3600 / 1000);
    }
}
