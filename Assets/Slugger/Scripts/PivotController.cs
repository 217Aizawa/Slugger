using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    //衝突判定デバッグ用プログラム
    //コントローラーを振って衝突判定のデバッグが手間なので自動スイングをさせる

    Rigidbody rb;
    Vector3 batOffset;
    float curretRotate;
    float maxAngle = -0.9f;
    float angleY = 180;//角速度180 360は貫通した
    float angleX = 45;
    float tm;
    float toMound = 18.44f;
    float releasePoint = 2.46f;
    float ballspeed = 80000;// 80km/hを秒速へ変換
    float timeSec = 3600;//
    float swingTime = 0.46f;
    //投球間隔 + マウンド間(m)　- 投球位置(m) / 80km/h  * 時間(h) - スイング時間（ミートまで）
    float swingTiming;// = 7.0f + (18.44f - 2.46f) / 80000 * 3600f -0.46f;
    float roopTime =7.0f;
    bool isCount;
    bool firstSwing = true;//初球用のbool
    bool isSwing = false;//スイングのオンオフ
    public bool isDebug = false;

    /****Gizmo表示用******/
    public bool isGizmo;
    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.red;
    /*********************/

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        batOffset = this.transform.eulerAngles;//角度取得
        //Debug.Log("SwingTime" + swingTiming);
        swingTiming = roopTime + (toMound - releasePoint) /ballspeed * timeSec - swingTime;

    }

    void FixedUpdate()
    {
        TmCounter();
        curretRotate = this.transform.rotation.y;
        //Debug.Log("angle" + curretRotate);

        if (Input.GetKey(KeyCode.Space))
        {
            isCount = true;
        }
        
        if (isDebug)//自動スイングのオンオフ
        {
            if (swingTiming <= tm && firstSwing)
            {
                tm = 0;
                isSwing = true;
                firstSwing = false;
                rb.angularVelocity = new Vector3(angleX, angleY, 0);//(15,180,0)
            }

            if (roopTime <= tm && !firstSwing)
            {
                tm = 0;
                isSwing = true;
                rb.angularVelocity = new Vector3(angleX, angleY, 0);//(15,180,0)
            }

            if (isSwing)//回転制限＆リセット
            {
                if (curretRotate <= maxAngle)//maxAngle =-0.9f
                {
                    Debug.Log("angle over");
                    rb.angularVelocity = Vector3.zero;
                    this.transform.eulerAngles = batOffset;
                    isSwing = false;
                }
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
        //Debug.Log("Time Count" + tm);
    }
}
;