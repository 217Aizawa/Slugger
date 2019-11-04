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
    Vector3 batPos;
    Vector3 ballPos;

    // Start is called before the first frame update
    void Start()
    {
        bat = GameObject.FindGameObjectWithTag("Bat");
        ball = this.gameObject;

        batRad = 0.0355f;//バットのColliderから参照
        ballDia = 0.0723f;
        collisonDist = batRad + ballDia;
    }

    // Update is called once per frame
    void Update()//FixedUpdate
    {
        batPos = bat.transform.position;
        ballPos = ball.transform.position;

        dist = Vector3.Distance(batPos, ballPos);
        Debug.Log(ballPos);
        //もしも、バットとボールの距離が batRad + ballDia 以下ならば...
        if (dist < collisonDist)
        {
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("collison");
        }

        /*if (Physics.Raycast(transform.position, transform.forward, 1))
        {
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("Ray hit");
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
}
