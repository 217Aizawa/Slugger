using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUserPos : MonoBehaviour
{
    GameObject user;
    Vector3 userPos;

    // Start is called before the first frame update
    void Start()
    {
        user = this.gameObject;
        userPos = user.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))//右座席用座標
        {
            user.transform.position = new Vector3(0.5f, userPos.y, userPos.z);
            Debug.Log("Left");
        }
        if (Input.GetKey(KeyCode.R))//左座席用座標
        {
            user.transform.position = new Vector3(-0.5f, userPos.y, userPos.z);
            Debug.Log("Right");
        }
    }
}
