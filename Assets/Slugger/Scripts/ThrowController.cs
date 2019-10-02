﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    Vector3 throwPos;//投球位置
    Vector3 target;//目標
    bool timeCheck = false;
    float time = 0;
    float angle;
    int speed = 22;// 5m/s = 18km/h, 22m/s = 79.2km/h
    public GameObject ball;
    public GameObject ballPrefab;
    void Start()
    {
        throwPos = ball.transform.position;
        BallReset();
        //Mathf.Asin(g * 1d / v / v) / 2
        angle = Mathf.Asin(9.81f * 0.6f * 18.44f / speed / speed) / 2;//打ち出し角度
    }

    void Update()
    {
        Debug.Log(angle);
        TimeCounter();
        if (Input.GetKey(KeyCode.Space))                                        //条件文をGameStateで書く
        {
            timeCheck = true;
            Throw();
        }

        if(3.0f <= time)
        {
            time = 0;
            timeCheck = true;
            //ball = Instantiate(ball, throwPos, Quaternion.identity);
            ball = Instantiate(ballPrefab, throwPos, Quaternion.identity);
            if (timeCheck)
                Throw();
        }
    }

    void TimeCounter()
    {
        Debug.Log("Time" + time);
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
        rb.velocity = new Vector3(0, speed * Mathf.Sin(angle), speed * Mathf.Cos(angle));
        //rb.velocity = transform.forward * speed;
    }

    private void BallReset()//ボール情報をリセットする
    {
        ball.transform.position = throwPos;
        Rigidbody rig = ball.GetComponent<Rigidbody>();
        rig.useGravity = false;
        rig.isKinematic = true;
        rig.velocity = Vector3.zero;
    }

    /*void Update()
    {
        TimeCounter();
        if (Input.GetKey(KeyCode.Space))                                        //条件文をGameStateで書く
        {
            timeCheck = true;
            Throw();
        }
        if (3.0f <= time)
        {
            timeCheck = !timeCheck;
            BallReset();
        }
    }*/
}
