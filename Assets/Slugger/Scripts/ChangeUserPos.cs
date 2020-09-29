using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUserPos : MonoBehaviour
{
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
            OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) //Xボタン
        {
            Debug.Log("");
            user.transform.position = new Vector3(0.5f, userPos.y, userPos.z);
        }
        //左座席用座標
        if (Input.GetKey(KeyCode.L) ||
            OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)) //Yボタン
        {
            user.transform.position = new Vector3(-0.5f, userPos.y, userPos.z);
        }
    }
}
