using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;

    Vector3 velocity = Vector3.zero;
    public float smoothFactor = 0.15f;

    int offset;

    private void Start()
    {
        offset = (int)transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 newPostion = m_target.position;
        newPostion.y = transform.position.y;
        newPostion.z += offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPostion, ref velocity, smoothFactor * Time.deltaTime);
        //Debug.Log(newPostion);
    }

}
