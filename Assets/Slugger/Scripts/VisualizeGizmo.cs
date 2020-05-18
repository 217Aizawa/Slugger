using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeGizmo : MonoBehaviour
{
    public GameObject childBat;
    public bool isGizmo;
    public float gizmoSize = 0.3f;
    public Color gizmoColor = Color.red;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        if (isGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, gizmoSize);
        }
    }

}
