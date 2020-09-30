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
        //右コントローラーはバットに設置するので、正しくは左コントローラーで打席切り替えを行う
        //今の設定では左コントローラーは非表示になっているので表示できるようにし、打席切り変えコードを変更する
        //右座席用座標
        if (Input.GetKey(KeyCode.R) ||
            OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)) //Xボタン
        {
            user.transform.position = new Vector3(0.5f, userPos.y, userPos.z);
        }
        //左座席用座標
        if (Input.GetKey(KeyCode.L) ||
            OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch)) //Yボタン
        {
            user.transform.position = new Vector3(-0.5f, userPos.y, userPos.z);
        }


        /*右コントローラー用*/
        //右コントローラーはバットに設置するので基本使用しない
        //右座席用座標
        /*
        if (Input.GetKey(KeyCode.R) ||
            OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) //Aボタン
        {
            user.transform.position = new Vector3(0.5f, userPos.y, userPos.z);
        }
        //左座席用座標
        if (Input.GetKey(KeyCode.L) ||
            OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)) //Bボタン
        {
            user.transform.position = new Vector3(-0.5f, userPos.y, userPos.z);
        }*/
    }
}
