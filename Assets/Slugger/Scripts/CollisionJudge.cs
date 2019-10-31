using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionJudge : MonoBehaviour
{
    GameObject bat;
    GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        bat = this.GetComponent<GameObject>();
        ball = GameObject.Find("ball");
    }

    // Update is called once per frame
    void FixeedUpdate()
    {
        /*if (Physics.Raycast(transform.position, transform.forward, 2))
        {
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("Ray hit");
        }*/
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bat")
        {
            //Debug.Log("OnCollisionEnter");
            this.gameObject.GetComponent<CorrectPhysics>().Disable();
            Debug.Log("Called Disable");
        }
    }
}
