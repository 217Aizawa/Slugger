using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCameraPos : MonoBehaviour
{
    GameObject cam;
    Vector3 camPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        camPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))//右座席用座標
        {
            cam.transform.position = new Vector3(0.5f, camPos.y, camPos.z);
            Debug.Log("Left");
        }
        if (Input.GetKey(KeyCode.R))//左座席用座標
        {
            cam.transform.position = new Vector3(-0.5f, camPos.y, camPos.z);
            Debug.Log("Right");
        }
    }
}
