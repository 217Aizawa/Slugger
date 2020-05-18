﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    float startRotate = 0;
    float curretRotate;
    float speed = 30f;
    float minAngle = 0;
    float maxAngle = 120;
    float yRoatate = 0;
    float angleY;
    public GameObject childBat;

    /****Gizmo表示用******/
    public bool isGizmo;
    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.red;

    public bool isSwing = false;//スイングのオンオフ

    void Start()
    {
        
    }

    void Update()
    {
        curretRotate = this.transform.rotation.y;
        angleY = Mathf.Clamp(curretRotate + speed, minAngle, maxAngle);

        //時間経過でisSwingのオンオフを制御
        /*
        if (Input.GetKey(KeyCode.S) || isSwing)//一度スイングしたら回転をリセット
        {
            Debug.Log("swing");
            yRoatate = Mathf.Clamp(rotationSpd * Time.deltaTime, 0, maxRotate);
            //this.transform.Rotate(0, rotationSpd, 0);
            //this.transform.Rotate(0, 0, 0);//リセット
            transform.eulerAngles = new Vector3(0, yRoatate, 0);
        }*/

        if (Input.GetKey(KeyCode.R) || isSwing)//一度スイングしたら回転をリセット
        {
            Debug.Log("swing");
            //transform.rotation = Quaternion.Euler(0, angleY, 0);
            this.transform.Rotate(0, angleY, 0);//speed
            if(maxAngle <= curretRotate)
            {
                Debug.Log("angle over");
                speed = 0;
            }
            //this.transform.Rotate(0, 0, 0);//リセット
        }

    }

    void OnDrawGizmos()
    {
        if (isGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, gizmoSize);
        }
    }

}
;