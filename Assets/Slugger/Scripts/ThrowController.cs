using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private Vector3 throwPos;//投球位置
    private Vector3 target;//目標
    private int speed = 5;
    private bool timeCheck = false;
    public GameObject ball;
    float time = 0;
    public GameObject nextBall;//次に生成するボール
    
    void Start()
    {
        throwPos = ball.transform.position;//投球位置をボールのある位置に設定する。
    }

    void Update()
    {
        //Rigidbody rig = ball.GetComponent<Rigidbody>();
        TimeCounter();
        if (Input.GetKey(KeyCode.Space))
        {
            timeCheck = true;
            Throw();
        }
        if(3.0f <= time)
        {
            //ball.SetActive(false); クローンまで非表示なってしまう
            //Destroy(ball);
            //既存のボールを投球後にInstantiateでボールを生成し、投げる。を繰り返す。
            //ball = Instantiate(ball, throwPos, Quaternion.identity);
            timeCheck = !timeCheck;
            /*ball.transform.position = throwPos;//投球位置に戻す
            rig.useGravity = false;
            rig.isKinematic = true;*/
            BallReset();
        }
    }
    public void TimeCounter()
    {
        Debug.Log("Time" + time);
        if (timeCheck)
        {
            time += Time.deltaTime;
            //Debug.Log("Time" + time);
        }
        else
        {
            time = 0;
        }
    }

    public void Throw()//投球関数
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = transform.forward * speed;//速度を加算
    }

    private void BallReset()//投球後にボールの情報をリセットする
    {
        ball.transform.position = throwPos;
        Rigidbody rig = ball.GetComponent<Rigidbody>();
        rig.useGravity = false;
        rig.isKinematic = true;
        rig.velocity = Vector3.zero;
    }
}
