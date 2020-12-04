using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    //ThrowControllerオブジェクトに添付済み
    //Unityの物理演算を使用投球制御用プログラム 
    //CorrectPhysicsでは自前の物理演算を実装している
    //一定の時間間隔、速度で投球を行う

    Vector3 throwPos;//投球位置

    float ballSpeed;
    float time = 0;
    float angle;//投げ出し角度

    int speed = 22;//22m/s = 79.2km/h

    bool timeCheck = false;//時間計測のフラグ

    public GameObject ball;//Sceneにあるボール
    public GameObject ballPrefab;//設計図としてのボール（オリジナル）

    void Start()
    {
        Rigidbody rigid = ball.GetComponent<Rigidbody>();
        throwPos = ball.transform.position;
        BallReset();
        //      Mathf.Asin(重力加速度 * 割合(0.6) * 距離 /　ベクトル / ベクトル) / 2;
        angle = Mathf.Asin(9.81f * 0.68f * 18.44f / speed / speed) / 2;//
        Debug.Log("throw angle" + angle);
    }
 
    void FixedUpdate()
    {
        //Debug.Log("angle" + angle);
        //Debug.Log("ballSpeed" + ballSpeed + "km");
        TimeCounter();

        //投球開始
        if (Input.GetKey(KeyCode.Space))
        {
            timeCheck = true;
            Throw();
        }

        //投球ループ
        if (7.0f <= time)
        {
            time = 0;
            timeCheck = true;
            ball = Instantiate(ballPrefab, throwPos, Quaternion.identity);
            //Destroy(ball, 3.0f);

            if (timeCheck)
                Throw();
        }
    }

    void TimeCounter()//時間計測
    {
        if (timeCheck)
            time += Time.deltaTime;
        else
            time = 0;
    }

    private void Throw()//投球関数
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = new Vector3(0, speed * Mathf.Sin(angle), speed * Mathf.Cos(angle));//1.5fを除く
        ball.GetComponent<CorrectPhysics>().Enable(rb.velocity);//ballコンポーネントから取得
        Destroy(ball, 3.0f);
        //ballSpeed = rb.velocity.magnitude * 3600 / 1000;//速度を秒速から時速に変換する
        //Debug.Log("ballSpeed" + ballSpeed);
    }

    private void BallReset()//ボール情報をリセットする
    {
        ball.transform.position = throwPos;
        Rigidbody rig = ball.GetComponent<Rigidbody>();
        rig.useGravity = false;
        rig.isKinematic = true;
        rig.velocity = Vector3.zero;
    }
}
