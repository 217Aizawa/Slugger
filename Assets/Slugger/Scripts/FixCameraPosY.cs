using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;


public class FixCameraPosY : MonoBehaviour
{
    Vector3 cameraPos ;
    // Start is called before the first frame update
    void Start()
    {
        //cameraPos = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
        cameraPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = cameraPos;
        
    }
}
