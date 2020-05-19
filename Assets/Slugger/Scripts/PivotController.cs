using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    Rigidbody rb;

    float startRotate = 0;
    float curretRotate;
    float speed = 30f;
    float minAngle = 0;
    float maxAngle = 120;
    float yRoatate = 0;
    float angleY = 180;
    float angleX = 15;

    public bool isSwing = false;//スイングのオンオフ

    /****Gizmo表示用******/
    public bool isGizmo;
    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.red;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        curretRotate = this.transform.rotation.y;
        Debug.Log("angle" + this.transform.rotation.y);
        //angleY = Mathf.Clamp(180, minAngle, maxAngle);

        //時間経過でisSwingのオンオフを制御

        if (Input.GetKey(KeyCode.S) || isSwing)//一度スイングしたら回転をリセット  || isSwing
        {
            Debug.Log("swing");
            rb.angularVelocity = new Vector3(angleX, angleY, 0);
            if(0.9f <= curretRotate)//
            {
                Debug.Log("angle over");
                rb.angularVelocity = Vector3.zero;
                isSwing = false;
            }
        }
        ////////////////////////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.S) || isSwing)//一度スイングしたら回転をリセット
        {
            //Debug.Log("swing");
            //this.transform.Rotate(0, 120, 0);//speed
            //transform.rotation = Quaternion.Euler(0, angleY, 0);
            //transform.eulerAngles = new Vector3(0,angleY,0);
            //this.transform.Rotate(0, rotationSpd, 0);
            //this.transform.Rotate(0, 0, 0);//リセット
            //transform.eulerAngles = new Vector3(0, yRoatate, 0);
        }
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

}
;