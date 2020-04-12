using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUserPos : MonoBehaviour
{
    GameObject user;
    Vector3 userPos;

    void Start()
    {
        user = this.gameObject;
        userPos = user.transform.position;
    }

    void Update()
    {
        //右座席用座標
        if (Input.GetKey(KeyCode.L))                                                        // || OVRInput.GetDown(OVRInput.Button.Three)) //Three = Xボタン
        {
            user.transform.position = new Vector3(0.5f, userPos.y, userPos.z);
        }
        //左座席用座標
        if (Input.GetKey(KeyCode.R))                                                        // || OVRInput.GetDown(OVRInput.Button.Four)) //Four = Yボタン
        {
            user.transform.position = new Vector3(-0.5f, userPos.y, userPos.z);
        }
    }
}
