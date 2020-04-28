using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator animator;
    float time;
    bool timeCount;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Counter();//タイミング調整用関数

        //animator.SetTrigger("Idle");
        if (Input.GetKey(KeyCode.Space))
        {
            timeCount = true;
            Debug.Log(time);
        }

        if (5.23f <= time)
        {
            animator.SetTrigger("Play");
            Debug.Log("Play anim");
        }
        else
        {
            //timeCount = false;
        }

    }

    void Counter()
    {
        if (timeCount)
            time += Time.deltaTime;
        else
            time = 0;
    }
}
