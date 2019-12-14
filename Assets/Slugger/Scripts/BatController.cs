using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    /*Vector3 latestPos;//過去位置
    Vector3 speed;*/

    public static GameObject bat;
    public GameObject emptyBat;//バットの軌道表示専用
    Rigidbody batRb;

    Vector3 latestPos;
    Vector3 batGripLocal;
    Vector3 batDirLocal;

    public static Vector3 batGrip;
    public static Vector3 batDir;
    public static Vector3 swingSpeed;

    void Start()
    {
        batRb = GetComponent<Rigidbody>();
        batDirLocal = new Vector3(0, 0.9f, 0);//バットの長さ=90cm
        batGripLocal = new Vector3(0, -0.45f, 0);
        swingSpeed = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        swingSpeed = ((transform.position - latestPos) / Time.deltaTime);

        batDir = transform.TransformDirection(batDirLocal);//向きだけ変換する
        batGrip = transform.TransformPoint(batGripLocal);
        //Debug.Log("World :" + batDir + " Local :" + batDirLocal);
        //Debug.Log("World :" + batGrip + " Local :" + batGripLocal);
        Debug.Log("magnitude" + swingSpeed.magnitude);
        if (5 < swingSpeed.magnitude)
        {
            Instantiate(emptyBat, transform.position, transform.rotation);
        }
        latestPos = transform.position;
    }
}
