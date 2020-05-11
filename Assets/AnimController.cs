using System.Collections;
using System.Collections.Generic;
using UnityEngine;[RequireComponent(typeof(Rigidbody))]

public class AnimController : MonoBehaviour
{
    public static readonly string throwTmp = "Throw";// トリガー名
    //public static readonly string idleTmp = "Idle";
    private static int thrTrigger = Animator.StringToHash(throwTmp);
    //private static int idleTrigger = Animator.StringToHash(idleTmp);

    public GameObject ethan;
    //[SerializeField, Header("Animator")]
    //private Animator m_Animator;
    Animator animator;
    Vector3 offsetPos; //new Vector3(-0.1f, -0.13f, 0.954f);

    float time;
    float playTiming = 7.0f - 53.0f / 30.0f; //53.0f（投球間 - フレーム / フレームレート）投球に合わせたアニメーションの再生タイミング
    float roopTime = 7.0f;//アニメーションのループ間隔
    bool timeCount;//カウントフラグ
    bool firstBall = true;//初球のフラグ

    int ballCnt;//球数
    private int idle;
    //private int waitState;//ハッシュ値格納変数


    // Start is called before the first frame update
    void Start()
    {
        offsetPos = ethan.transform.position;
        animator = GetComponent<Animator>();

        //ステート確認用ハッシュ値
        idle = Animator.StringToHash("Base Layer.Idle");
    }

    // Update is called once per frame
    void Update()
    {
        Counter();
        //Debug.Log("time" + time);

        if (Input.GetKey(KeyCode.Space))
        {
            timeCount = true;
        }
        
        //初球の再生タイミング
        if(playTiming <= time && firstBall)
        {
            time = 0;
            animator.SetTrigger(thrTrigger);
            Debug.Log("Play anim");
            firstBall = false;//フラグオフ
        }

        //ループ用スクリプト
        if(animator.GetCurrentAnimatorStateInfo(0).fullPathHash == idle)//idle状態ならば
        {
            //Idle状態のアニメーションが動かないことが前提に位置をリセット
            //ethan.transform.position = offsetPos;
            //位置のリセットをAnimationEventでできないか確認する
            ResetPos();
        }
            
        //初球以降の再生タイミング
        if (roopTime <= time && !firstBall)
        {
            time = 0;
            animator.SetTrigger(thrTrigger);
        }
        

        /*
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == throwTrigger)
        {
            animator.Play("Idle", 0 , 0.0f);//Idleアニメーションの再生
            Debug.Log("Confirmed Hash");//Hash値は確認できた
        }*/
    }

    /*void LateUpdate()
    {
        ethan.transform.position = offsetPos;
        //Idleステートからの再開
        animator.Play("Idle");//Idleアニメーションの再生
    }*/

    void FirstThrow()
    {
        if (playTiming <= time && firstBall)//投球に合わせたアニメーションの再生タイミング
        {
            time = 0;
            //timeCount = false;//リセット
            animator.SetTrigger("Throw");
            Debug.Log("Play anim");
            firstBall = false;//フラグオフ
        }

    }

    void NextThrow()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == idle)//wait状態ならば
        {
            //イーサンの座標リセット
            ethan.transform.position = offsetPos;

        }
        if (7.0f <= time && !firstBall)//7.0f - 68.0f / 30.0f <= time && nextBall
        {
            time = 0;
            //Idleステートからの再開
            animator.Play("Throw");//Idleアニメーションの再生
        }
    }

    void Counter()
    {
        if (timeCount)
            time += Time.deltaTime;
        else
            time = 0;
    }

    public void ResetPos()
    {
        ethan.transform.position = offsetPos;//オフセットは取れてる
        Debug.Log("reset pos" + ethan.transform.position);
    }
}
