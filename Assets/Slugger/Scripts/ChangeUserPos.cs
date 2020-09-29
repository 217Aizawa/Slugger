using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUserPos : MonoBehaviour
{
    //打席切り替え用プログラム
    GameObject user;
    Vector3 userPos;

    void Start()
    {
        user = this.gameObject;
        userPos = user.transform.position;
    }

    void Update()
    {
        //右座席用座標
        if (Input.GetKey(KeyCode.R) ||
            OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) //Aボタン
        {
            Debug.Log("");
            user.transform.position = new Vector3(0.5f, userPos.y, userPos.z);
        }
        //左座席用座標
        if (Input.GetKey(KeyCode.L) ||
            OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)) //Bボタン
        {
            user.transform.position = new Vector3(-0.5f, userPos.y, userPos.z);
        }
    }
}
