﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 batOffset;
    float curretRotate;
    float maxAngle = -0.9f;
    float angleY = 180;//角速度180 360は貫通した
    float angleX = 45;
    float tm;
    //投球間隔 + マウンド間(m)　- 投球位置(m) / 80km/h  * 時間(h) - スイング時間（角速度）
    float swingTiming = 7.0f + (18.44f - 2.46f) / 80000 * 3600f -0.46f;
    float roopTimeSw =7.0f;
    bool isCount;
    bool firstSwing = true;//初球用のbool
    public bool isSwing = false;//スイングのオンオフ

    /****Gizmo表示用******/
    public bool isGizmo;
    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.red;
    /*********************/

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        batOffset = this.transform.eulerAngles;//角度取得
        Debug.Log("SwingTime" + swingTiming);
    }

    void Update()
    {
        TmCounter();
        curretRotate = this.transform.rotation.y;
        //Debug.Log("angle" + curretRotate);

        if (Input.GetKey(KeyCode.Space))
        {
            isCount = true;
        }
        
        if(swingTiming <= tm && firstSwing)
        {
            tm = 0;
            isSwing = true;
            firstSwing = false;
            rb.angularVelocity = new Vector3(angleX, angleY, 0);//(15,180,0)
        }

        if(roopTimeSw <= tm && !firstSwing)
        {
            tm = 0;
            isSwing = true;
            rb.angularVelocity = new Vector3(angleX, angleY, 0);//(15,180,0)
        }

        if (isSwing)//回転制限＆リセット
        {
            if (-0.9f >= curretRotate)//this.transfrom.rotation.y
            {
                Debug.Log("angle over");
                rb.angularVelocity = Vector3.zero;
                this.transform.eulerAngles = batOffset;
                isSwing = false;
            }
        }
        /*
        //時間経過でisSwingのオンオフを制御
        if (Input.GetKey(KeyCode.Space) || isSwing)//一度スイングしたら回転をリセット  || isSwing
        {
            Debug.Log("swing");
            isCount = true;
            isSwing = true;
            rb.angularVelocity = new Vector3(angleX, angleY, 0);//(15,180,0)
            if (-0.9f >= curretRotate)//this.transfrom.rotation.y
            {
                Debug.Log("angle over");
                rb.angularVelocity = Vector3.zero;
                this.transform.eulerAngles = batOffset;
                isSwing = false;
            }
        }*/
    }


    void OnDrawGizmos()
    {
        if (isGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, gizmoSize);
        }
    }

    void TmCounter()
    {
        if (isCount)
        {
            tm += Time.deltaTime;
        }
        else
        {
            tm = 0;
        }
        Debug.Log("Time Count" + tm);
    }
}
;