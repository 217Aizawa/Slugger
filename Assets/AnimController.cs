using System.Collections;
using System.Collections.Generic;
using UnityEngine;[RequireComponent(typeof(Rigidbody))]

public class AnimController : MonoBehaviour
{
    public GameObject ethan;
    //[SerializeField, Header("Animator")]
    //private Animator m_Animator;
    Animator animator;
    Vector3 offsetPos; //new Vector3(-0.1f, -0.13f, 0.954f);

    float time;
    float playTiming = 7.0f - 53.0f / 30.0f; //（投球間 - フレーム / フレームレート）投球に合わせたアニメーションの再生タイミング

    bool timeCount;//カウントフラグ
    bool firstBall = true;//初球のフラグ　初球以外はループ用の秒数で回す
    bool nextBall = false;//初球以降のフラグ

    int ballCnt;//球数
    private int idle;
    private int throwTrigger;//186524081
    private int waitState;//ハッシュ値格納変数


    // Start is called before the first frame update
    void Start()
    {
        offsetPos = ethan.transform.position;
        animator = GetComponent<Animator>();
        //throwTrigger = Animator.StringToHash("Base Layer.Throw");
        //waitState = Animator.StringToHash("Base Layer.Wait");
        idle = Animator.StringToHash("Base Layer.Idle");
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(throwTrigger);//ハッシュ値
        Counter();
        Debug.Log("time" + time);
        if (Input.GetKey(KeyCode.Space))
        {
            timeCount = true;
        }
        
        //初球の再生タイミング
        if (playTiming <= time && firstBall)//投球に合わせたアニメーションの再生タイミング
        {
            time = 0;
            nextBall = true;
            animator.SetTrigger("Throw");
            Debug.Log("Play anim");
            firstBall = false;//フラグオフ
        }

        
        //ループ用スクリプト
        if(animator.GetCurrentAnimatorStateInfo(0).fullPathHash == idle)//wait状態ならば
        {
            //イーサンの座標リセット
            ethan.transform.position = offsetPos;
            
        }
        if (7.0f <= time && nextBall)//7秒間隔で繰り返し
        {
            time = 0;
            //Idleステートからの再開
            animator.Play("Throw");//Idleアニメーションの再生
        }

        /*
       if(7.0f - 68.0f / 30.0f <= time &&
            animator.GetCurrentAnimatorStateInfo(0).fullPathHash == waitState　)//wait状態ならば
        {
            //イーサンの座標リセット
            ethan.transform.position = offsetPos;
            //Idleステートからの再開
            animator.Play("Idle");//Idleアニメーションの再生
        }
         */

        /*
        //throwTrifggerではアニメ0ションが再生されない
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
            nextBall = true;
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
        if (7.0f <= time && nextBall)//7.0f - 68.0f / 30.0f <= time && nextBall
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
}
