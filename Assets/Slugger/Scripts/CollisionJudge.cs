using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionJudge : MonoBehaviour
{
    //public BatController batController;

    GameObject bat;
    GameObject ball;

    //SphereCollider ballCollider;
    float radius;
    float batRad;//バット半径
    float ballDia;//ボールの直径
    float collisonDist;
    float dist = 0;
    float enter = 0;

    Vector3 batPos1;//現在位置
    Vector3 ballPos1;

    Vector3 batPos0;//過去の位置
    Vector3 ballPos0;

    Vector3 batLine;//バットの線分
    Vector3 ballLine;//ボールの線分

    //Vector3 ballRelLine;//バットがbatPos0の時のボールの相対運動
    Vector3 ballRelLine1;//バットがbatPos1の時のボールの相対運動
    Vector3 forExhibition = new Vector3(0,15,-80);//展示用

    Vector3 nearBallPoint;
    Vector3 nearBatPoint;

    Vector3 P;
    Vector3 globalP;
    Vector3 conversion = new Vector3(-1,1,-1);//座標変換用
    
    Vector3 prevGrip;//過去位置
    Vector3 prevHead;

    Ray ray;
    RaycastHit hit;

    public AudioClip sound;//効果音を指定
    AudioSource audioSource;

    //[SerializeField] bool isHit = false;//二度打ちを防ぐためのbool
    Rigidbody rb;

    void Start()
    {
        radius = this.transform.lossyScale.x * 0.5f;
        audioSource = GetComponent<AudioSource>();
        //ballCollider = GetComponent<SphereCollider>();
        //Debug.Log("ballCollider" + ballCollider.radius);
        batRad = 0.0355f;//バットのColliderから参照
        ballDia = 0.0723f;// 0.07165 = バット半径＋ボール半径
        collisonDist = batRad + ballDia / 2 * 25f; //collisonDist = batRad + ballDia / 2 * 25;
        //collisonDist = bat.GetComponent<CapsuleCollider>().radius;//Colliderの半径を取得
        //Debug用
        //bat = GameObject.FindGameObjectWithTag("Bat");
        ball = this.gameObject;
        //Debug.Log("Collisiondist" + collisonDist);
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()//FixedUpdate
    {
        //本番用
        /*if (Input.GetKey(KeyCode.C))//要修正
        {
            bat = GameObject.FindGameObjectWithTag("Bat");
            //ball = this.gameObject;
        }*/

        if (GameObject.FindGameObjectWithTag("Bat"))
        {
            bat = GameObject.FindGameObjectWithTag("Bat");
            //ball = this.gameObject;
        }

        batPos1 = bat.transform.position;//現在位置
        ballPos1 = ball.transform.position;//現在位置

        batLine = batPos1 - batPos0;
        ballLine = ballPos1 - ballPos0;
        
        //ballRelLine = ballLine - batLine;
        ballRelLine1 = ballLine + batLine;

        Vector3 s = ballPos0 + Vector3.Cross(BatController.batDir, ballRelLine1);//S（外積）
        Plane p = new Plane(ballPos0, ballPos1 - batLine, s);//三つの点から面を作成
        Vector3 n = p.normal;//面の法線
        float t = Vector3.Dot(ballPos0 - BatController.batGrip, n) / Vector3.Dot(BatController.batDir, n);
        Vector3 q = BatController.batGrip + t * BatController.batDir;//球の平面に対し、バットが一番近くなる点

        //垂線の足の座標
        Vector3 perpendicularFootPoint = Vector3.Project(q - ballPos0, ballRelLine1);//qからballRelLine1に下した
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
            nearBallPoint = ballPos0 + perpendicularFootPoint;
        }

        if (t < 0)//batの最近点を求める
        {
            nearBatPoint = BatController.batGrip;
        }
        else if (t > 1)
        {
            nearBatPoint = BatController.batGrip +  BatController.batDir;//バットの根本からbatDirまで行った座標
        }
        else
        {
            nearBatPoint = q;
        }

        /*********************************************************************************************************/
        //スイングに対応した当たり判定パート
        //バットの４点から三角平面を二つ作成し、ボールとの交点を求める
        Plane g1 = new Plane(BatController.batHead, prevHead, BatController.batGrip);//h1,h0,g1の三角形
        Plane g0 = new Plane(BatController.batHead, prevHead, prevGrip);//h1,h0,g0の三角形

        Vector3 d1 = Vector3.Cross(BatController.batHead - BatController.batGrip, prevHead - BatController.batGrip);
        Vector3 d0 = Vector3.Cross(BatController.batHead - prevGrip, prevHead - prevGrip);

        //float s1 = d1.magnitude / 2;//面積
        //float s0 = d0.magnitude / 2;
        /*Vector3 spd = ((BatController.batHead - prevHead) / Time.fixedDeltaTime);//移動速度
        Vector3 weidth = spd * Time.fixedDeltaTime;//距離（底辺）*/
        Vector3 weidth = BatController.batHead - prevHead;//h0 - h1までのベクトル（長さ）
        //Vector3 height = 
        ray = new Ray(ballPos0, ballRelLine1);//rayの設定

        //Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f, false);

        //Debug.Log("Line" + ballRelLine1);
        //Debug.DrawLine(BatController.batHead, prevHead);
        //Debug.DrawLine(prevHead, BatController.batGrip);
        //Debug.DrawLine(BatController.batGrip, BatController.batHead);
        /*********************************************************************************************************/




        //スイングに対応した当たり判定
        /*****************************************************************************************************/

        //ray.direction.magnitude;//ballRelLine1.magnitude;

        /*①*/
        if (g1.Raycast(ray, out dist))//作成した平面までの距離を返す
        {
            //Debug.Log(dist);
            if(dist < ballRelLine1.magnitude)//ボールが平面を横切るか？
            {
                Debug.Log("Crossed");//スイング時には消え、固定時には消えなかった
                Destroy(ball);
            }
        }
        
        /*
        if(Physics.SphereCast(ray, ballDia/2, out hit, ballRelLine1.magnitude))
        {
            Gizmos.DrawRay(ray);
            Gizmos.DrawWireSphere(ballPos0, ballDia / 2);//ballCollider.radius
            if (dist < ballRelLine1.magnitude)//ボールが平面を横切るか？
            {
                Debug.Log("Crossed");//スイング時には消え、固定時には消えなかった
                Destroy(ball);
            }
        }*/
        /*****************************************************************************************************/


        if (collisonDist > (nearBatPoint - nearBallPoint).magnitude && rb.isKinematic)                                                                                    //1 >= flag collisonDist
        {
            //Debug.Log("Plane1" + g1);
            //Debug.Log("Plane0" + g0);
            //Debug.Log("s1" + s1);//面積
            //Debug.Log("s0" + s0);
            //Debug.Log("W" + weidth);//.magnitude
            if (Physics.Raycast(ray, out hit, ballRelLine1.magnitude))//ballPos0, ballRelLine1                                                         //ballRelLine1
            {
                if (hit.collider.tag == "Bat")
                {
                    this.gameObject.GetComponent<CorrectPhysics>().Disable();
                    P = Vector3.Project(hit.point - BatController.batGrip, BatController.batDir) + BatController.batGrip;                               //hit.pointからbatDirに下した
                    gameObject.GetComponent<Rigidbody>().AddForce( (hit.point - P).normalized * 1.6f * 
                        (BatController.swingSpeed - ball.GetComponent<Rigidbody>().velocity ).magnitude, ForceMode.Impulse);
                    
                    //動画用
                    //gameObject.GetComponent<Rigidbody>().AddForce((hit.point - P).normalized * 100 * CorrectPhysics.batSpeed.magnitude, ForceMode.Impulse);


                    audioSource.PlayOneShot(sound);//一度再生する
                    //Debug.Log("hitPoint:" + hit.point + "P:" + P);
                    //Debug.Log("batSpeed magnitude " + BatController.swingSpeed.magnitude);
                    //Debug.Log("batSpeed magnitude " + CorrectPhysics.batSpeed.magnitude);
                }
            }
        }

        /*過去位置*/
        prevGrip = BatController.batGrip;
        prevHead = BatController.batHead;
        ballPos0 = ballPos1;
        batPos0 = batPos1;
    }
    /*
    void OnDrawGizmos()
    {

        if (Physics.SphereCast(ray, ballDia / 2, out hit, ballRelLine1.magnitude))//ballDia / 2
        {
            Gizmos.DrawRay(ray);
            Gizmos.DrawWireSphere(ballPos0, ballDia / 2);//ballCollider.radius
            //Debug.Log("Spherecast on");
            if (dist < ballRelLine1.magnitude)//ボールが平面を横切るか？
            {
                Debug.Log("Crossed");//スイング時には消え、固定時には消えなかった
                Destroy(ball);
            }
        }
        else
        {
            //Gizmos.DrawRay(ray);
            //Gizmos.DrawWireSphere(ballPos0, ballDia / 2);//ballCollider.radius
        }
    }*/

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

