using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    /*Vector3 latestPos;//過去位置
    Vector3 speed;*/

    public GameObject bat;
    Rigidbody batRb;
    Vector3 batGripLocal;
    Vector3 batDirLocal;
    public static Vector3 batGrip;
    public static Vector3 batDir;

    void Start()
    {
        batRb = GetComponent<Rigidbody>();
        batDirLocal = new Vector3(0, 0.9f, 0);//バットの長さ=90cm
        batGripLocal = new Vector3(0, -0.45f, 0);
    }

    void FixedUpdate()
    {
        batDir = transform.TransformDirection(batDirLocal);//向きだけ変換する
        batGrip = transform.TransformPoint(batGripLocal);
        //Debug.Log("World :" + batDir + " Local :" + batDirLocal);
        //Debug.Log("World :" + batGrip + " Local :" + batGripLocal);

        /*speed = ((bat.transform.position - latestPos) / Time.deltaTime);
        latestPos = bat.transform.position;
        Debug.Log("batSpeed" + speed);*/
    }
}
