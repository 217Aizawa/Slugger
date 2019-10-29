using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 p0;//position
    private Vector3 v0;//vector ThrowController > rb.velocity
    private float t0;//time 
    private bool isKinematic;
    private bool isEanbled = false;

    //RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isEanbled)
            return;

        float dt = Time.time - t0;//経過時間
        transform.position = p0 + v0 * dt - 0.5f * Vector3.up  * 9.8f * dt * dt;//物理演算
    }

    public void Disable()//kinemativ無効
    {
        rb.isKinematic = isKinematic;
        isEanbled = false;
        //バットと衝突した瞬間に新たな方向に速度を加える
        //rb.velocity = new Vector3();
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

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter");
        Disable();
        Debug.Log("Called Disable");
    }

}
//                 開始位置　　　　　　向き　　　　　　　長さ（レイ）
/*if(Physics.Raycast(transform.position, transform.forward, 2))
{
    Disable();
    Debug.Log("Ray hit");
}*/
