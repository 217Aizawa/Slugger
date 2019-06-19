using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPos : MonoBehaviour
{
    //GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        //controller = GameObject.FindGameObjectWithTag("Controller");
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.position);
    }
}
