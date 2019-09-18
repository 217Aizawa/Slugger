using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private Vector3 throwPos;//投球位置
    private Vector3 target;//目標
    private Vector3 ballSpeed =Vector3.forward * 5.0f;
    public GameObject ball;
    GameObject nextBall;//次に生成するボール
    // Start is called before the first frame update
    void Start()
    {
        throwPos = ball.transform.position;//投球位置をボールのある位置に設定する。
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            throwfnc();
        }
        //既存のボールを投球後にInstantiateでボールを生成し、投げる。を繰り返す。
        //nextBall = Instantiate(ball, throwPos, Quaternion.identity) as GameObject;
    }

    private void throwfnc()//投球関数
    {
        ball.GetComponent<Rigidbody>().velocity = ballSpeed;//速度を加算
    }
}
