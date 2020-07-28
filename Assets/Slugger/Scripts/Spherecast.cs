using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spherecast : MonoBehaviour
{
    float radius;
    Vector3 p1;
    Vector3 p2;

    void Start()
    {
        radius = this.transform.lossyScale.x * 0.5f;
    }

    void Update()
    {
        RaycastHit hit;
        p1 = transform.position;
        Ray ray = new Ray(p1, transform.forward);
        float distanceToObjecct = 0;
        if (Physics.SphereCast(ray, radius, out hit, 5))
        {
            distanceToObjecct = hit.distance;
            Debug.Log("Update" + distanceToObjecct);
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        p2 = transform.position;
        Ray ray = new Ray(p2,transform.forward);
        float distanceToObject = 0;
        if (Physics.SphereCast(ray, radius, out hit, 5))
        {
            distanceToObject = hit.distance;
            Debug.Log("FixedUpdate" + distanceToObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(p1, transform.forward * 5);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(p1 + transform.forward * 5, radius);
    }
}
