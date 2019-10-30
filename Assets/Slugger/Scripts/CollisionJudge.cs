using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionJudge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter");
        this.gameObject.GetComponent<CorrectPhysics>().Disable();
        Debug.Log("Called Disable");
    }
}
