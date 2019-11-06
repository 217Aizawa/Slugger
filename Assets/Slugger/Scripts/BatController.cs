using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    //Vector3 swingSpeed;
    public GameObject Bat;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3(1, 2, 3);
        Vector3 nlz = pos.normalized;
        Debug.Log("nlz" + nlz.magnitude);
        Debug.Log("mgn" + pos.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("BatPos" + Bat.transform.position);
    }

    /*void OnCollisionEnter(Collision collision)
    {

    }*/
}
