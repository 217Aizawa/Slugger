using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    //Vector3 swingSpeed;
    public GameObject Bat;
    Vector3 batGripLocal;
    Vector3 batDirLocal;
    public static Vector3 batGrip;
    public static Vector3 batDir;
    // Start is called before the first frame update
    void Start()
    {
        /*Vector3 pos = new Vector3(1, 2, 3);
        Vector3 nlz = pos.normalized;
        Debug.Log("nlz" + nlz.magnitude);
        Debug.Log("mgn" + pos.magnitude);*/
        batDirLocal = new Vector3(0, 0.9f, 0);//バットの向き90cm
        batGripLocal = new Vector3(0, -0.45f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        batDir = transform.TransformDirection(batDirLocal);//向きだけ変換する
        batGrip = transform.TransformPoint(batGripLocal);

        //Debug.Log(" batGrip :" + batGrip + "batDir :" + batDir);

        //Debug.Log("BatPos" + Bat.transform.position);
    }

    /*void OnCollisionEnter(Collision collision)
    {

    }*/
}
