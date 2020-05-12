using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInputFilter : IPlatformInput
{
    Vector3 m_vectorMovement;

    public void GetPlayerMove(Rigidbody m_rigidbody, Transform transform, VJHandle m_movementVJ)
    {
        float m_moveHorizontal = m_movementVJ.InputDirection.x;
        float m_moveVertical = m_movementVJ.InputDirection.y;
        m_vectorMovement.Set(m_moveHorizontal, 0, m_moveVertical);
        m_vectorMovement = m_vectorMovement.normalized * Time.deltaTime * 5;
        m_rigidbody.MovePosition(transform.position + m_vectorMovement);
    }

    public void GetPlayerShoot()
    {
        if(Input.GetKey(KeyCode.Space) && Platform.ANDROID)
            PlayerWeaponHandler.s_instance.currentWeapon.HandleShootInputs();
    }
}
