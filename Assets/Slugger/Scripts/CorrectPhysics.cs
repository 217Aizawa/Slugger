using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPhysics : MonoBehaviour
{
    public GameObject bat;

    private Rigidbody rb;
    Rigidbody batRb;

    private Vector3 p0;//position
    private Vector3 v0;//vector ThrowController > rb.velocity
    private float t0;//time 
    private bool isKinematic;
    private bool isEanbled = false;

    public float k = 10000;//10000


    // Start is called before the first frame update
    void Start()
    {
        bat = GameObject.FindGameObjectWithTag("Bat");
        batRb = bat.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isEanbled)
            return;

        float dt = Time.time - t0;//経過時間
        transform.position = p0 + v0 * dt - 0.5f * Vector3.up  * 9.8f * dt * dt;//物理演算
        rb.velocity = v0 - Vector3.up * 9.8f * dt;
    }
    //rb.velocity = v0 + k * rb.mass * batSpeed * Mathf.Cos(angle) / batRb.mass;
    //kinemativ無効　バットと衝突した瞬間に新たな方向に速度を加える
    public void Disable()
    {
        rb.isKinematic = isKinematic;
        isEanbled = false;//transformでの移動オフ        
        Vector3 batSpeed = batRb.velocity;
        //Debug.Log(batSpeed);
        if (batSpeed.sqrMagnitude == 0)
        {
            batSpeed = -Vector3.forward;
        }
        //Debug.Log("batSpeed" + batSpeed);
        float cos = Vector3.Dot(-rb.velocity.normalized, batSpeed.normalized);
        Debug.Log("rb.velocity.normalized" + rb.velocity.normalized);
        Debug.Log("batSpeed.normalized" + batSpeed.normalized);
        Debug.Log("cos" + cos);
        //Vector3 direction = v0 + k * rb.mass * (batSpeed - v0) * cos / batRb.mass;
        Vector3 direction = v0 + k * batRb.mass * (batSpeed - v0) * cos / rb.mass;
        rb.AddForce(direction, ForceMode.VelocityChange);
        //rb.AddForce(direction, ForceMode.Impulse);
        Debug.Log("direction" + direction);//zの値が-で打者から見て前方に飛ぶ
    }



    //Throw関数内から呼び出す(isKinematicをオンの状態で投球している)
    public void Enable(Vector3 v)
    {
        rb = GetComponent<Rigidbody>();//基の状態を記憶
        isKinematic = rb.isKinematic;//初期状態を記憶
        rb.isKinematic = true;
        p0 = transform.position;
        v0 = v;
        t0 = Time.time;//ゲームスタートからの経過時間
        isEanbled = true;
    }
}
//                 開始位置　　　　　　向き　　　　　　　長さ（レイ）
/*if(Physics.Raycast(transform.position, transform.forward, 2))
{
    Disable();
    Debug.Log("Ray hit");
}*/
