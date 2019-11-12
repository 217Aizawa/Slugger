using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public GameObject Bat;
    Vector3 batGripLocal;
    Vector3 batDirLocal;
    public static Vector3 batGrip;
    public static Vector3 batDir;

    void Start()
    {
        batDirLocal = new Vector3(0, 0.9f, 0);//バットの長さ=90cm
        batGripLocal = new Vector3(0, -0.45f, 0);
    }

    void FixedUpdate()
    {
        batDir = transform.TransformDirection(batDirLocal);//向きだけ変換する
        batGrip = transform.TransformPoint(batGripLocal);
        //Debug.Log("World :" + batDir + " Local :" + batDirLocal);
        Debug.Log("World :" + batGrip + " Local :" + batGripLocal);
    }
}
