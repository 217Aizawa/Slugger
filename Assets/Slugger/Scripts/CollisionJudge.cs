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
    float testDist = 0.2f;
    Vector3 batPos1;//現在位置
    Vector3 ballPos1;

    Vector3 batPos0;//過去の位置
    Vector3 ballPos0;

    Vector3 batLine;//バットの線分
    Vector3 ballLine;

    Vector3 ballRelLine;//ボールの相対運動
    Vector3 batDirLocal = new Vector3(0, 0.9f ,0);//バットの向き90cm
    Vector3 batGripLocal = new Vector3(0, -0.45f,0);//グリップ位置
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug用
        bat = GameObject.FindGameObjectWithTag("Bat");
        ball = this.gameObject;

        batRad = 0.0355f * 10f;//バットのColliderから参照
        ballDia = 0.0723f;
        collisonDist = batRad + ballDia;
    }

    // Update is called once per frame
    void FixedUpdate()//FixedUpdate
    {

        //Vector3 batDir = transform.TransformDirection(batDirLocal);//向きだけ変換する
        //Vector3 batGrip = transform.TransformDirection(batGripLocal);

        //本番用
        if (Input.GetKey(KeyCode.C))//要修正
        {
            bat = GameObject.FindGameObjectWithTag("Bat");
            ball = this.gameObject;
        }

        batPos1 = bat.transform.position;
        ballPos1 = ball.transform.position;
        
        batLine = batPos1 - batPos0;
        ballLine = ballPos1 - ballPos0;

        ballRelLine = ballLine - batLine;
        //Debug.Log("batPos" + batPos1);


        //dist = Vector3.Distance(ballPos1, batPos1);
        Vector3 s = ballPos0 + Vector3.Cross(BatController.batDir, ballRelLine);//S動かし三つの点を求める
        Plane p = new Plane(ballPos0,ballPos1 - batLine,s);//面を作成
        Vector3 n = p.normal;//面の法線
        float t = Vector3.Dot(ballPos0 - BatController.batGrip, n) / Vector3.Dot(BatController.batDir, n);
        Vector3 q = BatController.batGrip + t * BatController.batDir;//球の平面に対し、バットが一番近くなる点
        //Debug.Log("s" + s);
        //Debug.Log("p" + p);
        //Debug.Log("n" + n);
        //Debug.Log("t" + t);
        //Debug.Log("q" + q);
        //Debug.Log("ballPos :" + ballPos0 + " batGrip :" + BatController.batGrip + "batDir :" + BatController.batDir);
        
        //垂線の足の座標
        Vector3 perpendicularFootPoint = Vector3.Project(q - ballPos0, ballRelLine);
        float flag = Vector3.Dot(ballRelLine, perpendicularFootPoint);//-なら通過 +なら先にある

        Vector3 nearBallPoint;
        Vector3 nearBatPoint; 


        if(flag < 0)//ballの最近点を求める
        {
            nearBallPoint = ballPos0;
        }
        else if((perpendicularFootPoint - ballPos0).sqrMagnitude > ballRelLine.sqrMagnitude)
        {
            nearBallPoint = ballPos0 + ballRelLine;
        }
        else
        {
            nearBallPoint = perpendicularFootPoint;
        }

        if(t < 0)//batの最近点を求める
        {
            nearBatPoint = BatController.batGrip;
        }
        else if(t > 1)
        {
            nearBatPoint = BatController.batDir;
        }
        else
        {
            nearBatPoint = q;
        }


        //Debug.Log("nearPoint" + (nearBatPoint - nearBallPoint).magnitude);
        //Debug.Log("nearBatPoint" + nearBatPoint);
        //Debug.Log("nearBallPoint" + nearBallPoint);
        //Debug.Log("nearPoint" + (nearBatPoint - nearBallPoint).magnitude);



        if (collisonDist > (nearBatPoint - nearBallPoint).magnitude)//1 >= flag collisonDist
         {
            /*Debug.Log("collisonDist" + collisonDist);
            Debug.Log("nearPoint" + (nearBatPoint - nearBallPoint).magnitude);
            Debug.Log("nearBatPoint" + nearBatPoint);
            Debug.Log("nearBallPoint" + nearBallPoint); */
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("Called Disable");
         }

        ballPos0 = ballPos1;
        batPos0 = batPos1;
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bat")
        {
            //Debug.Log("OnCollisionEnter");
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("Called Disable");
        }
    }*/

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
