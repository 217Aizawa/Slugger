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
    Vector3 ballLine;//ボールの線分

    Vector3 ballRelLine;//バットがbatPos0の時のボールの相対運動
    Vector3 ballRelLine1;//バットがbatPos0の時のボールの相対運動
    Vector3 forExhibition = new Vector3(0,15,-80);

    Vector3 nearBallPoint;
    Vector3 nearBatPoint;

    RaycastHit hit;

    void Start()
    {
        //Debug用
        //bat = GameObject.FindGameObjectWithTag("Bat");
        //ball = this.gameObject;

        batRad = 0.0355f;//バットのColliderから参照
        ballDia = 0.0723f;
        // 0.07165 = バット半径＋ボール半径
        collisonDist = batRad + ballDia / 2 * 25;
        //collisonDist = bat.GetComponent<CapsuleCollider>().radius;//Colliderの半径を取得
    }

    void FixedUpdate()//FixedUpdate
    {
        //本番用
        if (Input.GetKey(KeyCode.C))//要修正
        {
            bat = GameObject.FindGameObjectWithTag("Bat");
            ball = this.gameObject;
        }

        if (GameObject.FindGameObjectWithTag("Bat"))
        {
            bat = GameObject.FindGameObjectWithTag("Bat");
            ball = this.gameObject;
        }

        batPos1 = bat.transform.position;//現在位置
        ballPos1 = ball.transform.position;//現在位置

        batLine = batPos1 - batPos0;
        ballLine = ballPos1 - ballPos0;

        ballRelLine = ballLine - batLine;
        ballRelLine1 = ballLine + batLine;

        Vector3 s = ballPos0 + Vector3.Cross(BatController.batDir, ballRelLine1);//S動かし三つの点を求める（外積）
        Plane p = new Plane(ballPos0, ballPos1 - batLine, s);//面を作成
        Vector3 n = p.normal;//面の法線
        float t = Vector3.Dot(ballPos0 - BatController.batGrip, n) / Vector3.Dot(BatController.batDir, n);
        Vector3 q = BatController.batGrip + t * BatController.batDir;//球の平面に対し、バットが一番近くなる点

        //垂線の足の座標
        Vector3 perpendicularFootPoint = Vector3.Project(q - ballPos0, ballRelLine1);
        float flag = Vector3.Dot(ballRelLine1, perpendicularFootPoint);//-なら通過 +なら先にある

        if (flag < 0)//ballの最近点を求める
        {
            nearBallPoint = ballPos0;
        }
        else if ((perpendicularFootPoint - ballPos0).sqrMagnitude > ballRelLine1.sqrMagnitude)
        {
            nearBallPoint = ballPos0 + ballRelLine1;
        }
        else
        {
            nearBallPoint = perpendicularFootPoint;
        }

        if (t < 0)//batの最近点を求める
        {
            nearBatPoint = BatController.batGrip;
        }
        else if (t > 1)
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
            if (Physics.Raycast(ballPos0, ballRelLine1, out hit, ballRelLine1.magnitude + 3))//ballRelLine1
            {
                if (hit.collider.tag == "Bat")
                {
                    this.gameObject.GetComponent<CorrectPhysics>().Disable();
                    Vector3 P = Vector3.Project(hit.point - BatController.batGrip, BatController.batDir);
                    gameObject.GetComponent<Rigidbody>().AddForce((transform.position - P) * 2, ForceMode.Impulse);//.normalized * 5
                }
            }
        }
        //gameObject.GetComponent<Rigidbody>().AddForce(forExhibition, ForceMode.Impulse);//展示用

        ballPos0 = ballPos1;
        batPos0 = batPos1;

        /*private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag == "Bat")
            {
                //Debug.Log("OnCollisionEnter");
                this.gameObject.GetComponent<CorrectPhysics>().Disable();
                Debug.Log("Called Disable");
            }
        }*/
    }
}
