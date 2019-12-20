using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    /*Vector3 latestPos;//過去位置
    Vector3 speed;*/
    public GameObject emptyBat;
    GameObject model;
    public static GameObject bat;
    Rigidbody batRb;

    Vector3 latestPos;
    Vector3 batGripLocal;
    Vector3 batDirLocal;
    Vector3 batHeadLocal;

    Vector3 prevBatGrip;
    Vector3 prevBatDir;
    Vector3 prevBatHead;
    public bool isBat = false;//バットの軌道表示

    public static Vector3 batHead;
    public static Vector3 batGrip;
    public static Vector3 batDir;
    public static Vector3 swingSpeed;

    void Start()
    {
        batRb = GetComponent<Rigidbody>();
        batDirLocal = new Vector3(0, 0.9f, 0);//バットの長さ=90cm
        batGripLocal = new Vector3(0, -0.45f, 0);
        batHeadLocal = new Vector3(0, 0.45f, 0);
        swingSpeed = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        swingSpeed = ((transform.position - latestPos) / Time.deltaTime);

        batDir = transform.TransformDirection(batDirLocal);//向きだけ変換する
        batGrip = transform.TransformPoint(batGripLocal);//グリップ位置
        batHead = transform.TransformPoint(batHeadLocal);//ヘッド位置

        //Debug.Log("World :" + batDir + " Local :" + batDirLocal);
        //Debug.Log("World :" + batGrip + " Local :" + batGripLocal);

        //Debug.Log("magnitude" + swingSpeed.magnitude);

        if (5 < swingSpeed.magnitude && isBat)
        {
            model = Instantiate(emptyBat, transform.position,transform.rotation);
        }
        Destroy(model, 2f);
        
        /**********************過去位置**************************/
        prevBatDir = batDir;
        prevBatGrip = batGrip;
        prevBatHead = batHead;
        latestPos = transform.position;
        /********************************************************/
    }
}
