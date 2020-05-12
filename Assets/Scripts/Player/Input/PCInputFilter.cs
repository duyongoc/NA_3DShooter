using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputFilter : IPlatformInput
{
    Vector3 m_vectorMovement;

    public void GetPlayerMove(Rigidbody m_rigidbody, Transform transform, VJHandle m_movementVJ)
    {
        // move
        float m_moveHorizontal = Input.GetAxisRaw("Horizontal");
        float m_moveVertical = Input.GetAxisRaw("Vertical");
        m_vectorMovement.Set(m_moveHorizontal, 0, m_moveVertical);
        m_vectorMovement = m_vectorMovement.normalized * Time.deltaTime * 5;
        m_rigidbody.MovePosition(transform.position + m_vectorMovement);

    }

    public void GetPlayerShoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        // if (Physics.Raycast(ray, out hit, 100, m_layerMask))
        // {
        //     Vector3 playerToMouse = hit.point - transform.position;
        //     playerToMouse.y = 0f;
        //     Quaternion rotation = Quaternion.LookRotation(playerToMouse);
        //     m_rigidbody.MoveRotation(rotation);
        // }

        if(Input.GetMouseButtonDown(0))
        {
            PlayerWeaponHandler.s_instance.currentWeapon.HandleShootInputs();
        }
    }
}
