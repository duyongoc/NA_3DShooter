using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;
    private Vector3 m_offset;

    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2f;
    
    private float currentZoom = 10f;
    public float smoothFactor = 0.5f;
    public bool lookAtPlayer = false;

    private void Start()
    {
        m_offset = transform.position - m_target.position;
    }

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void LateUpdate()
    {
        Vector3 newPostion = m_target.position + m_offset;
        transform.position = Vector3.Slerp(transform.position, newPostion, smoothFactor);

        if(lookAtPlayer)
            transform.LookAt(m_target);
    }

}
