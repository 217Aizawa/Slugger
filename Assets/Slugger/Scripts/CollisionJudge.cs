using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionJudge : MonoBehaviour
{
    GameObject bat;
    GameObject ball;
    float batRad;//バット半径
    float ballDia;//ボールの直径
    float collisonDist;
    float dist;

    Vector3 batPos1;
    Vector3 ballPos1;

    Vector3 batPos0;
    Vector3 ballPos0;

    Vector3 batLine;//バットの線分
    Vector3 ballLine;//ボールの線分

    // Start is called before the first frame update
    void Start()
    {
        //Debug用
        bat = GameObject.FindGameObjectWithTag("Bat");
        ball = this.gameObject;

        batRad = 0.0355f;//バットのColliderから参照
        ballDia = 0.0723f;
        collisonDist = batRad + ballDia;
        Debug.Log("batPos" + batPos1);
    }

    // Update is called once per frame
    void Update()//FixedUpdate
    {
        //本番用
        if (Input.GetKey(KeyCode.C))
        {
            bat = GameObject.FindGameObjectWithTag("Bat");
            ball = this.gameObject;
        }
        Debug.Log("batPos" + batPos1);

        batPos1 = bat.transform.position;
        ballPos1 = ball.transform.position;
        
        batLine = batPos1 - batPos0;
        ballLine = ballPos1 - ballPos0;

        Vector3 s = ballPos0 + Vector3.Cross(batLine, ballLine);//S動かし三つの点を求める
        Plane p = new Plane(ballPos0,ballPos1,s);//面を作成
        Vector3 n = p.normal;//面の法線
        float t = Vector3.Dot(ballPos0, n) / Vector3.Dot(batLine, n);
        Vector3 q = ballPos0 + t * ballLine;//衝突する点

        //垂線の足の座標
        Vector3 perpendicularFootPoint = Vector3.Project(q - ballPos0, batPos0 - ballPos1);

        ballPos0 = ballPos1;
        batPos0 = batPos1;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bat")
        {
            //Debug.Log("OnCollisionEnter");
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("Called Disable");
        }
    }

    /*Vector3 HitPoint(Vector3 a, Vector3 b, Vector3 p0, Vector3 p1)//
    {
        return a + Vector3.Project(p0 - a, b - a);
    }*/

    /*Vector3 HitPoint(Vector3 a, Vector3 b, Vector3 p0, Vector3 p1)
    {
        return a + Vector3.Project(p1 - p0, b - a);
    }*/

    //Debug.Log("batPos" + batPos1);


        //dist = Vector3.Distance(batPos1, ballPos1);
        //Debug.Log(ballPos);
        //もしも、バットとボールの距離が batRad + ballDia 以下ならば...
        /*if (dist < collisonDist)
        {
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("collison");
        }*/
}
