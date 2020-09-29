using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpherecast : MonoBehaviour
{
    //Spherecastでどこまで衝突検知をしているかを確認する

    float radius;

    RaycastHit hit;

    [SerializeField]
    bool isEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        radius = this.transform.lossyScale.x * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //　SphereCastのレイを飛ばしターゲットと接触しているか判定
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, radius, out hit, transform.forward.magnitude))
        {
            Debug.Log(hit.transform.name);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, radius);
        Gizmos.DrawRay(transform.position, transform.forward);
    }

}
