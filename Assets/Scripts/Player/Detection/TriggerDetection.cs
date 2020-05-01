using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    private Detection m_dectection;

    private void Start()
    {
        m_dectection = this.GetComponentInParent<Detection>();
    }

    private void Update()
    {
        foreach(Detection enemy in m_dectection.detected)
        {
            if(enemy == null)
            {
                m_dectection.detected.Remove(enemy);
            }
            //else if(enemy.health == 0) m_dectection.detected.Remove(enemy);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Enemy") && other != null)
        {
            m_dectection.detected.Add(other.GetComponent<Detection>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        m_dectection.detected.Remove(other.GetComponent<Detection>());
    }
    
}
