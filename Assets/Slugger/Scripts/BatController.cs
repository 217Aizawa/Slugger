﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    //Vector3 swingSpeed;
    public GameObject Bat;

    // Start is called before the first frame update
    void Start()
    {
        Bat = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("BatPos" + Bat.transform.position);
    }

    /*void OnCollisionEnter(Collision collision)
    {

    }*/
}
