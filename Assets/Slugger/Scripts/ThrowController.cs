using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private Vector3 throwPos;//投球位置
    private Vector3 target;//目標
    private Vector3 ballSpeed =Vector3.forward * 5.0f;
    private int speed = 5;
    private bool timeCheck = false;
    public GameObject ball;
    float time = 0;
    GameObject nextBall;//次に生成するボール

    void Start()
    {
        throwPos = ball.transform.position;//投球位置をボールのある位置に設定する。
    }

    void Update()
    {
        GameObject balls = GameObject.FindGameObjectWithTag("Ball");
        TimeCounter();
        if (Input.GetKey(KeyCode.Space))
        {
            timeCheck = true;
            throwfnc();
        }
        if(3.0f <= time)
        {
            //ball.SetActive(false);
            Destroy(ball);
            //既存のボールを投球後にInstantiateでボールを生成し、投げる。を繰り返す。
            //nextBall = 
            ball = Instantiate(ball, throwPos, Quaternion.identity);
            //Rigidbody prefabRb = nextBall.GetComponent<Rigidbody>();
            //prefabRb.useGravity = false;
            //prefabRb.isKinematic = true;
            timeCheck = !timeCheck;
        }
    }

    private void throwfnc()//投球関数
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = transform.forward * speed;//速度を加算
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
}
