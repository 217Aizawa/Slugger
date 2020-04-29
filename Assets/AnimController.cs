using System.Collections;
using System.Collections.Generic;
using UnityEngine;[RequireComponent(typeof(Rigidbody))]

public class AnimController : MonoBehaviour
{
    [SerializeField, Header("Animator")]
    private Animator m_Animator;
    Animator animator;

    float time;
    float result = 7.0f - 53.0f / 30.0f; 
    bool timeCount;//カウントフラグ
    bool firstBall;//初球のフラグ　初球以外はループ用の秒数で回す

    private int throwTrigger;//186524081

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        throwTrigger = Animator.StringToHash("Base Layer.Throw");
        Debug.Log("Time" + result);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(throwTrigger);//ハッシュ値
        Counter();

        if (Input.GetKey(KeyCode.Space))
        {
            timeCount = true;
        }
        //投球間隔、フレーム、フレームレート
        if (7.0f - 53.0f / 30.0f <= time)//投球に合わせたアニメーションの再生タイミング
        {
            //animator.SetBool("isThrow", true);
            animator.SetTrigger("Throw");//"Throw"　throwTrigger
            Debug.Log("Play anim");
        }

        //throwTrifggerではアニメ0ションが再生されない
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == throwTrigger)
        {
            Debug.Log("Confirmed Hash");//Hash値は確認できた
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
