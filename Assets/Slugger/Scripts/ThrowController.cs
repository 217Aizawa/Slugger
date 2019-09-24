﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    Vector3 throwPos;//投球位置
    Vector3 target;//目標
    bool timeCheck = false;
    float time = 0;
    int speed = 5;
    public GameObject ball;

    void Start()
    {
        throwPos = ball.transform.position;
    }

    void Update()
    {
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
            BallReset();
            if (timeCheck)
            {
                Throw();
            }
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
        rb.velocity = transform.forward * speed;//速度を加算
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
