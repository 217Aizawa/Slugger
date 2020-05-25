using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 batOffset;
    float curretRotate;
    float maxAngle = -0.9f;
    float angleY = 360;//角速度 before180
    float angleX = 45;
    float tm;
    bool isCount;
    public bool isSwing = false;//スイングのオンオフ

    /****Gizmo表示用******/
    public bool isGizmo;
    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.red;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        batOffset = this.transform.eulerAngles;//角度取得

    }

    void Update()
    {
        curretRotate = this.transform.rotation.y;
        Debug.Log("angle" + curretRotate);
        //angleY = Mathf.Clamp(180, minAngle, maxAngle);

        //時間経過でisSwingのオンオフを制御
        if (Input.GetKey(KeyCode.A))
            isSwing = true;

        if (Input.GetKey(KeyCode.S) || isSwing)//一度スイングしたら回転をリセット  || isSwing
        {
            Debug.Log("swing");
            isSwing = true;
            rb.angularVelocity = new Vector3(angleX, angleY, 0);//(15,180,0)
            //transform.eulerAngles = new Vector3(angleX, angleY, 0);
            /*if(0.8f <= curretRotate)//
            {
                Debug.Log("angle over");
                rb.angularVelocity = Vector3.zero;
                isSwing = false;
            }*/
            if (-0.9f >= curretRotate)//
            {
                Debug.Log("angle over");
                rb.angularVelocity = Vector3.zero;
                this.transform.eulerAngles = batOffset;
                isSwing = false;
            }
        }
        ////////////////////////////////////////////////////////////////////////
        //if (Input.GetKey(KeyCode.S) || isSwing)//一度スイングしたら回転をリセット
        //{
            //Debug.Log("swing");
            //this.transform.Rotate(0, 120, 0);//speed
            //transform.rotation = Quaternion.Euler(0, angleY, 0);
            //transform.eulerAngles = new Vector3(0,angleY,0);
            //this.transform.Rotate(0, rotationSpd, 0);
            //this.transform.Rotate(0, 0, 0);//リセット
            //transform.eulerAngles = new Vector3(0, yRoatate, 0);
        //}
        /////////////////////////////////////////////////////////////////////////////////
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
        tm += Time.deltaTime;
    }
}
;